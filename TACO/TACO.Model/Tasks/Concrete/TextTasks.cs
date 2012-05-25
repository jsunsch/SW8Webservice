using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TACO.Model.Tasks.Abstract;
using TACO.Model.Domain;
using Excolo.Architecture.Core.NHibernate;

namespace TACO.Model.Tasks.Concrete
{
    public class TextTasks : ITextTasks
    {
        private NHibernateRepository<Text> repos = new NHibernateRepository<Text>();

        public Text CreateText(string textName, string textContent, POI associatedPOI)
        {
            Text text = new Text { TextName = textName, TextContent = textContent, POI = associatedPOI };

            using (repos.DBContext.BeginTransaction())
            {
                try
                {
                    repos.SaveOrUpdate(text);
                    repos.DBContext.SubmitChanges();
                    repos.DBContext.CommitTransaction();
                }
                catch
                {
                    repos.DBContext.RollbackTransaction();
                    text = null;
                }
            }

            return text;
        }
    }
}
