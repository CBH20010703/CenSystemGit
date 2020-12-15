using System;

namespace CenBolgsSystem.Models
{
    public class BlogsPlant
    {
        public bool RemoveLeave(BlogsLeave data)
        {
            using (db_CenSystemEntities db = new db_CenSystemEntities())
            {
                try
                {

                    return db.SaveChanges() <= 0 ? false : true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
        }
    }
}