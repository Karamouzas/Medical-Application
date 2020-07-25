using ModelsLib;
using ModelsLib.Models;
using ModelsLib.ServerConnect;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace PatientAppLib.Services
{
    public class AllergyService
    {
        public AllergyService()
        {
            var client = new HttpClient();
            
        }

        public async Task<List<Allergy>> GetPatientAllergiesAsync()
        {

            var srv = new ServerConnection();
            srv.ServerPath = "http://192.168.1.10/AsclepiusServices/";
            var response = await srv.RequestAsync<List<Allergy>>("Allergy/GetAllergies", ServerConnection.HttpMethod.POST);

            return response;
        } 
    }
}
