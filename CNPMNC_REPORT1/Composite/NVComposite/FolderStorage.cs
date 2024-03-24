﻿using System;
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
        private Dictionary<string, List<AComponent>> Folder;

        private FolderStorage()
        {
            Folder = new Dictionary<string, List<AComponent>>();
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

        public void SaveFolder(string folder, List<AComponent> files)
        {
            Folder.Add(folder, files);
        }

        public void RemoveFolder(string folder)
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

        public List<string> GetPageWithFolder(string folder)
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

        public Dictionary<string, List<AComponent>> GetAllFolder()
        {
            return Folder;
        }
    }
}