namespace Mobile_IP.Models;

public class DateVitale
{
    private double temp;
    private int umiditate;
    private int bpm;
    private int[] ekg = new int[124];

    public double Temp { get => temp; set => temp = value; }
    public int Umiditate { get=> umiditate; set => umiditate = value; }
    public int Bpm { get => bpm; set => bpm = value; }
    public int[] Ekg { get => ekg; set => ekg = value; }
}
