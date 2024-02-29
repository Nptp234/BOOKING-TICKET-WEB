using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CNPMNC_REPORT1.Composite
{
    public class FolderCompo : AbstractOne
    {
        List<AbstractOne> lsFolder;
        public FolderCompo(string name)
        {
            this.pageName = name;
            lsFolder = new List<AbstractOne>();
        }

        public void AddCompo(AbstractOne name)
        {
            lsFolder.Add(name);
        }

        public void RemoveCompo(AbstractOne name)
        {
            lsFolder.Remove(name);
        }

        public void RemoveAllCompo(Predicate<AbstractOne> name)
        {
            lsFolder.RemoveAll(name);
        }

        public override string ListPage()
        {
            if (lsFolder != null)
            {
                StringBuilder result = new StringBuilder();

                foreach (var file in lsFolder)
                {
                    result.Append(file.ListPage());
                }

                return result.ToString();
            }
            else return null;
        }

    }
}