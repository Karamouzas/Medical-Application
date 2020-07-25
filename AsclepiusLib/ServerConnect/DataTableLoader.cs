using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.Threading;

namespace ServerConnect
{
    public class DataTableLoader
    {
        #region Decalrations
        DataTable _dtLoaded;
        bool _Completed;
        Exception _exThrown;
        Thread thrSelect;
        #endregion

        #region Properties
        public OleDbCommand SelectCommand { get; set; }
        public bool Completed
        {
            get
            {
                return _Completed;
            }
        }
        public DataTable Table
        {
            get
            {
                return _dtLoaded;
            }
        }
        #endregion

        #region Public Functions
        public void LoadAsync()
        {
            thrSelect = new Thread(LoadDataTable);
            thrSelect.Start();
        }
        public void CancelLoad()
        {
            try
            {
                SelectCommand.Cancel();
                thrSelect.Abort();
                _dtLoaded.Dispose();
                _dtLoaded = null;
            }
            catch (Exception) { }
        }
        public void WaitToComplete(HttpContext currentContext = null)
        {
            if (currentContext == null)
                currentContext = HttpContext.Current;

            while (!Completed)
            {
                if (!currentContext.Response.IsClientConnected)
                {
                    CancelLoad();
                    break;
                }
                Thread.Sleep(500);
            }

            if (_exThrown != null)
                throw _exThrown;
        }
        #endregion

        #region Private Functions
        private void LoadDataTable()
        {
            try
            {
                using (OleDbDataAdapter da = new OleDbDataAdapter())
                {
                    _Completed = false;
                    _exThrown = null;
                    da.SelectCommand = SelectCommand;
                    _dtLoaded = new DataTable();
                    da.Fill(_dtLoaded);
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                _exThrown = ex;
            }
            _Completed = true;
        }
        #endregion        
    }
}