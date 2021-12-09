Create PROCEDURE [dbo].[Spgetstatistics]
AS
  BEGIN
      SELECT
Count(c.class_name)                                                           AS
'NoOfStudentsPerClass',
c.class_name
AS 'Class',
Count(cc.id)
'NoOfStudentsPerCountry',
cc.NAME
AS 'Country',
Dateadd(day, Avg(Datediff(day, '1900-01-01', s.date_of_birth)),
'1900-01-01') AS
'AvergeAgeOfStudents'
    FROM   [dbo].[students] (nolock) s
           LEFT JOIN [dbo].[classes] (nolock) c
                  ON c.id = s.class_id
           LEFT JOIN [dbo].[countries] (nolock) cc
                  ON cc.id = s.country_id
    GROUP  BY c.class_name,
              cc.NAME
    ORDER  BY 1 DESC
END 