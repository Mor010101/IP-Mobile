using Mobile_IP.Models;

namespace Mobile_IP.ViewModels
{
    public class PulsViewModel
    {
        public List<Puls> DateGrafic { get; set; }

        public PulsViewModel()
        {
            DateGrafic = new List<Puls>()
            {
                new Puls{ Timp = 0.0, Bataie = 0.0 },
                new Puls{ Timp = 0.1, Bataie = 0.1 },
                new Puls{ Timp = 0.2, Bataie = -0.1 },
                new Puls{ Timp = 0.3, Bataie = 0.1 },
                new Puls{ Timp = 0.4, Bataie = 0.0 },
                new Puls{ Timp = 0.5, Bataie = 0.0 },
                new Puls{ Timp = 0.6, Bataie = 0.0 },
                new Puls{ Timp = 0.7, Bataie = 0.1 },
                new Puls{ Timp = 0.8, Bataie = -0.1 },
                new Puls{ Timp = 0.9, Bataie = 0.1 },
                new Puls{ Timp = 1.0, Bataie = 0.0 }
            };
        }
    }
}
