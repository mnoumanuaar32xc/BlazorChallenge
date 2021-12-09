using BlazorCRUD.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCRUD.Seeder
{
    public class Seeder
    {
        private readonly AppDBContext _appDBContext;

        public Seeder(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task Seed()
        {
            await AddCountries();
            await AddClasses();

        }
        /// <summary>
        /// Seed method to add Superadmin
        /// </summary>
        /// <returns></returns>
        private async Task AddCountries()
        {

            if (!_appDBContext.countries.Any())
            {
                var _countries = new List<Countries>();
                _countries.Add(new Data.Countries { name = "Oman" });
                _countries.Add(new Data.Countries { name = "Pakistan" });
                _countries.Add(new Data.Countries { name = "India" });
                _countries.Add(new Data.Countries { name = "UAE" });
                _countries.Add(new Data.Countries { name = "China" });

                _appDBContext.AddRange(_countries);
                await _appDBContext.SaveChangesAsync();
            }
        }
        private async Task AddClasses()
        {

            if (!_appDBContext.classes.Any())
            {
                var _classes = new List<Classes>();
                _classes.Add(new Data.Classes { class_name = "KG" });
                _classes.Add(new Data.Classes { class_name = "Two" });
                _classes.Add(new Data.Classes { class_name = "Three" });
                _classes.Add(new Data.Classes { class_name = "Four" });
                _classes.Add(new Data.Classes { class_name = "Five" });
                _classes.Add(new Data.Classes { class_name = "Six" });
                _classes.Add(new Data.Classes { class_name = "Seven" });
                _classes.Add(new Data.Classes { class_name = "Eight" });
                _classes.Add(new Data.Classes { class_name = "Nine" });
                _classes.Add(new Data.Classes { class_name = "Ten" });

                _appDBContext.AddRange(_classes);
                await _appDBContext.SaveChangesAsync();
            }
        }
    }
}