using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccessLayer
{
    public class SettingsRepository:ConnectionClass
    {
        public decimal GetVatRate()
        {
            return (decimal)Entity.Settings.SingleOrDefault(s => !s.SettingsId).VatRate;
        }
    }
}
