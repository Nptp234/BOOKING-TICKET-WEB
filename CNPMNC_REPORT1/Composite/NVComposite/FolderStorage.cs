using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace CNPMNC_REPORT1.Composite.NVComposite
{
    public class FolderStorage
    {
        // Sử dụng singleton để không tạo lại danh sách folder
        private static FolderStorage instance;

        // Sử dụng dictionary vì một folder có danh sách file con
        private Dictionary<FolderNV, List<AComponent>> Folder;

        private FolderStorage()
        {
            Folder = new Dictionary<FolderNV, List<AComponent>>();
        }

        public static FolderStorage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FolderStorage();
                }
                return instance;
            }
        }

        public void SaveFolder(FolderNV folder, List<AComponent> files)
        {
            Folder.Add(folder, files);
        }

        public void RemoveFolder(FolderNV folder)
        {
            if (Folder.ContainsKey(folder))
            {
                Folder.Remove(folder);
            }
        }

        public void RemoveAll()
        {
            Folder.Clear();
        }

        public List<string> GetPageWithFolder(FolderNV folder)
        {
            List<string> pages = new List<string>();
            if (Folder.ContainsKey(folder))
            {
                foreach(var p in Folder[folder])
                {
                    pages.Add(p.GetPage().ToString());
                }
                return pages;
            }
            else return null;
        }

        public Dictionary<FolderNV, List<AComponent>> GetAllFolder()
        {
            return Folder;
        }
    }
}