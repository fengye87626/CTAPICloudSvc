using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DelphiCTCombinaiton.Models
{
    public class AuthToken
    {

        public string access_token { get; set; }  //access_token=N5IU0q0ECthkGbkPxGOFaFvOzecA2qxXXmFTAQyCYfZ5W5rMBAXUlActLTDJ9gBI9TiOtoulfZK845wxAGx6hBgvi8FiMh7hhIS762UUyNYE0RHto9PrYf9PYp-A-3JUZF7wH9AQ5mzGZVZcWcnoxpQiJmx4BObgVDDQzP1wrN0i3iKdvp0DtZQrRN4fSNnxPrgNKVLeZ9HFL08iSI12takxiBhUSJsPed1XW_oVs3-pgrYyIIs0qlE3ndHWooTrDmJuc9tWAaRdM2PWFEZscMLXS-rzNtC2hikl0-1zLTkXvedHM_HuwXna7DWeiqGB5RO-TjEcARPkk0CYC1-znv0F66wBbjdVN0p6dOxmV6uHaKNgmaVrcDp232HNEj89hqqaNnCxMl5JwmFS2rFSOCmbLwaTDcd58JEgaGiNDYIariTnDUR3IWCetxx9DcrsKwpQFtBKhGi6dWLEanvCiaNLxbgUTBGJBMBB0wkLnbYLjgq-YmxY-zDt1oJubge6M9qrZ6sb4qP9e5MxadnT_yRyg1hDrzt0LcNXNRLLiiPD7Q-BY3FQpKcyCXLG1XnyWMcc7iLHh8S5cIZGbJcsOxC8kPc

        public int expires_in { get; set; } //expires_in=3599

        public string refresh_token { get; set; } //refresh_token=c27d5e8d-9b1f-4ea5-8a7c-075fc2a27254

        public string token_type { get; set; } //token_type=bearer
    }
}
