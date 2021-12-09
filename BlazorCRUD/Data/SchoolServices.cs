using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorCRUD.Data
{
    public class SchoolServices
    {
        #region Property
        private readonly AppDBContext _appDBContext;
        private const string procedureForStatistics = "[dbo].[SPGetStatistics]";
        #endregion
        #region Constructor
        public SchoolServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        #endregion
        #region Get List of students
        public async Task<List<StudentModel>> GetAllStudentssAsync()
        {
            try
            {
                List<StudentModel> studentModelsList = new();
                // get all students 
                var result = await _appDBContext.students.Select(x => new { x.id, x.name, date_of_birth = x.date_of_birth.ToString(), x.country_id, x.class_id }).ToListAsync();
               // add in list all students
                foreach (var item in result)
                {
                    StudentModel _studentModel = new StudentModel();
                    var _classValue = await _appDBContext.classes.FirstOrDefaultAsync(c => c.id.Equals(item.class_id));
                    var _countryValue = await _appDBContext.countries.FirstOrDefaultAsync(c => c.id.Equals(item.country_id));
                    _studentModel.class_name = _classValue.class_name;
                    _studentModel.student_name = item.name;
                    _studentModel.student_id = item.id;
                    _studentModel.country_name = _countryValue.name;
                    _studentModel.date_of_birth = Convert.ToDateTime(item.date_of_birth);
                    studentModelsList.Add(_studentModel);

                }
                return studentModelsList;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Statistics to display on website 
        public async Task<List<Statistics>> GetStatisticsAsync()
        { 
            List<Statistics> _statisticsList = new();
            try
            {
                // call Store procedure and get Statistics 
                // store procedure are avilabe in path  BlazorCRUD\DB Script\SPGetStatistics.sql
                // Before Run App must execute procedure in Database.
                var _result = await Task.Run(() => RawSqlQuery($"{procedureForStatistics}", x => new Statistics
                {
                    NoOfStudentsPerClass = Convert.ToInt32(x[0]),
                    Class = x[1].ToString(),
                    NoOfStudentsPerCountry = Convert.ToInt32(x[2]),
                    Country = x[3].ToString(),
                    AverageAgeOfStudents = (DateTime?)x[4]
                }).ToList());
                _statisticsList = _result;

                return _statisticsList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Insert Students
        public async Task<bool> InsertStudentAsync(StudentModel _studentModel)
        {
            try
            {
                // students information save into database
                Students _students = new Students();
                _students.class_id = _studentModel.class_id;
                _students.country_id = _studentModel.country_id;
                _students.name = _studentModel.student_name;
                _students.date_of_birth = _studentModel.date_of_birth;
                await _appDBContext.students.AddAsync(_students);
                await _appDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
        #region Update Students
        public async Task<bool> UpdateStudentsAsync(StudentModel _studentModel)
        {
            // students information are update in database 
            Students _students = new Students() { id = _studentModel.student_id, class_id = _studentModel.class_id, country_id = _studentModel.country_id, date_of_birth = _studentModel.date_of_birth };
            _appDBContext.students.Update(_students);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
        #endregion
        #region DeleteEmployee
        public async Task<bool> DeleteStudentsAsync(StudentModel _studentModel)
        {
            try
            {
                // delete student record if exists
                Students students = new Students() { id = _studentModel.student_id, class_id = _studentModel.class_id, country_id = _studentModel.country_id, date_of_birth = _studentModel.date_of_birth };
                _appDBContext.students.Attach(students);
                _appDBContext.students.Remove(students);
                _appDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        #endregion
        #region Get Students by Id
        public async Task<Students> GetStudentsAsync(int _id)
        {
            // get user from database by student Id
            Students _students = await _appDBContext.students.FirstOrDefaultAsync(c => c.id.Equals(_id));
            return _students;
        }
        public async Task<StudentModel> GetStudentsModelAsync(int _id)
        {
            try
            {
                // get all student information
                StudentModel _studentModel = new StudentModel();
                var result = await _appDBContext.students.Where(c => c.id.Equals(_id)).Select(x => new { x.id, x.name, date_of_birth = x.date_of_birth.ToString(), x.country_id, x.class_id }).ToListAsync();
                foreach (var item in result)
                {
                    var _classValue = await _appDBContext.classes.FirstOrDefaultAsync(c => c.id.Equals(item.class_id));
                    var _countryValue = await _appDBContext.countries.FirstOrDefaultAsync(c => c.id.Equals(item.country_id));
                    _studentModel.country_id = _countryValue.id;
                    _studentModel.country_name = _countryValue.name;
                    _studentModel.class_id = _classValue.id;
                    _studentModel.class_name = _classValue.class_name;
                    _studentModel.student_name = item.name;
                    _studentModel.student_id = item.id;
                    _studentModel.country_name = _countryValue.name;
                    _studentModel.date_of_birth = Convert.ToDateTime(item.date_of_birth);

                }

                return _studentModel;
            }

            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion
        #region Dropdownlists
        public async Task<List<Countries>> GetAllCountriesAsync()
        {
            try
            {
                // countries dropdown list
                var result = await _appDBContext.countries.ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<Classes>> GetAllClassesAsync()
        {
            try
            {
                // classes dropdown list
                var result = await _appDBContext.classes.ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region  Raw query
        // use for Raw query 

        public List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> function)
        {
            using (var context = _appDBContext)
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    context.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        var entities = new List<T>();

                        while (result.Read())
                        {
                            entities.Add(function(result));
                        }
                        return entities;
                    }
                }
            }
        }
        #endregion
    }
}
