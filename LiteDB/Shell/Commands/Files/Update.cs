﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace LiteDB.Shell.Commands
{
    internal class FilesUpdate : Files, ICommand, IWebCommand
    {
        public bool IsCommand(StringScanner s)
        {
            return this.IsFileCommand(s, "update");
        }

        public void Execute(ref LiteEngine db, StringScanner s, Display display)
        {
            var id = this.ReadId(s);
            var file = db.FileStorage.FindById(id);

            if (file == null) return;

            file.Metadata = new JsonReader().ReadValue(s).AsObject;

            db.FileStorage.Update(file);

            display.WriteBson(file.AsDocument);
        }
    }
}
