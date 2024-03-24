using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Composite.NVComposite
{
    public class FolderNV : AComponent
    {
        private List<AComponent> Page = new List<AComponent>();
        private string FolderName { get; set; }

        public FolderNV() { }

        public FolderNV(string name)
        {
            FolderName = name;
        }

        public void SetName(string name)
        {
            FolderName = name;
        }

        public string GetName()
        {
            return FolderName;
        }

        public void AddPage(AComponent page)
        {
            Page.Add(page);
        }

        public void AddListPage(List<AComponent> pages)
        {
            Page = pages;
        }

        public void RemovePage(AComponent page)
        {
            Page.Remove(page);
        }

        public List<string> GetListStringPage()
        {
            List<string> pages = new List<string>();
            foreach(var p in Page)
            {
                pages.Add(p.ToString());
            }
            return pages;
        }

        public List<AComponent> GetListComponentPage()
        {
            return Page;
        }

        public override string GetPage()
        {
            return FolderName;
        }
    }
}