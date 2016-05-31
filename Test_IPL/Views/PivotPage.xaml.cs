using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Test_IPL.Views

{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class PivotPage : Page
    {
       public class Player
       {
            public String Name;
            public enum Team{ SRH,RCB,GL,KKR,MI,KXIP,RPS,DD}
            public Team TName;
            public enum Category { BAT,BOWL,WK,ALL}
            public Category CName;
            public bool Overseas;
            public String Image;
            public Int64 BasePrice;
            public Int32 Points;
       }
        
        public List<Player> playerList = new List<Player>();
        public List<Player> randomPlayerList = new List<Player>();
        public void binding()
        {
            playerList.Add(new Player { Name = "David Warner", TName = Player.Team.SRH, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/SRH/1.png", BasePrice = 17500000, Points = 95 });
            playerList.Add(new Player { Name = "Shikhar Dhawan", TName = Player.Team.SRH, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/SRH/2.png", BasePrice = 12500000, Points = 90 });
            playerList.Add(new Player { Name = "Kane Williamson", TName = Player.Team.SRH, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/SRH/3.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Eoin Morgan", TName = Player.Team.SRH, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/SRH/4.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Yuvraj Singh ", TName = Player.Team.SRH, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/SRH/5.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Moises Henriques", TName = Player.Team.SRH, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/SRH/6.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Ben Cutting", TName = Player.Team.SRH, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/SRH/7.png", BasePrice = 5000000, Points = 80 });
            playerList.Add(new Player { Name = "Deepak Hooda", TName = Player.Team.SRH, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/SRH/8.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Ashish reddy", TName = Player.Team.SRH, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/SRH/9.png", BasePrice = 3000000, Points = 70 });
            playerList.Add(new Player { Name = "Naman Ojha", TName = Player.Team.SRH, CName = Player.Category.WK, Overseas = false, Image = "/Assets/SRH/10.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Bipul Sharma", TName = Player.Team.SRH, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/SRH/11.png", BasePrice = 2000000, Points = 70 });
            playerList.Add(new Player { Name = "Bhuvaneshwar Kumar", TName = Player.Team.SRH, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/SRH/12.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Barinder Sran", TName = Player.Team.SRH, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/SRH/13.png", BasePrice = 5000000, Points = 80 });
            playerList.Add(new Player { Name = "Karn Sharma", TName = Player.Team.SRH, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/SRH/14.png", BasePrice = 5000000, Points = 80 });
            playerList.Add(new Player { Name = "Ashish Nehra", TName = Player.Team.SRH, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/SRH/15.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Trent Boult", TName = Player.Team.SRH, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/SRH/16.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Mustafizur Rahman", TName = Player.Team.SRH, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/SRH/17.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Ravindra Jadeja", TName = Player.Team.GL, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/GL/1.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Suresh Raina", TName = Player.Team.GL, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/GL/2.png", BasePrice = 15000000, Points = 95 });
            playerList.Add(new Player { Name = "Dwayne Bravo", TName = Player.Team.GL, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/GL/3.png", BasePrice = 12500000, Points = 90 });
            playerList.Add(new Player { Name = "Praveen Kumar", TName = Player.Team.GL, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/GL/4.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Dhawal kulkarni", TName = Player.Team.GL, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/GL/5.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Dinesh Karthik", TName = Player.Team.GL, CName = Player.Category.WK, Overseas = false, Image = "/Assets/GL/6.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Aaron Finch", TName = Player.Team.GL, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/GL/7.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Dale Steyn", TName = Player.Team.GL, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/GL/8.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Pradeep Sangwan", TName = Player.Team.GL, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/GL/9.png", BasePrice = 3000000, Points = 70 });
            playerList.Add(new Player { Name = "James Faulkner", TName = Player.Team.GL, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/GL/10.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Brendon Mccullum", TName = Player.Team.GL, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/GL/11.png", BasePrice = 12500000, Points = 90 });
            playerList.Add(new Player { Name = "Dwayne Smith", TName = Player.Team.GL, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/GL/12.png", BasePrice = 12500000, Points = 90 });
            playerList.Add(new Player { Name = "Pravin Tambe", TName = Player.Team.GL, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/GL/13.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Shivil Kaushik", TName = Player.Team.GL, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/GL/14.png", BasePrice = 2000000, Points = 65 });
            playerList.Add(new Player { Name = "Shadab Jakati", TName = Player.Team.GL, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/GL/15.png", BasePrice = 3000000, Points = 70 });
            playerList.Add(new Player { Name = "Ekalavya Dwivedi", TName = Player.Team.GL, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/GL/16.png", BasePrice = 2000000, Points = 65 });
            playerList.Add(new Player { Name = "Ishan Kishan", TName = Player.Team.GL, CName = Player.Category.WK, Overseas = false, Image = "/Assets/GL/17.png", BasePrice = 3000000, Points = 70 });
            playerList.Add(new Player { Name = "Murali Vijay", TName = Player.Team.KXIP, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/KXIP/1.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Wriddhiman Saha", TName = Player.Team.KXIP, CName = Player.Category.WK, Overseas = false, Image = "/Assets/KXIP/2.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "David Miller", TName = Player.Team.KXIP, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/KXIP/3.png", BasePrice = 10000000, Points = 90 });
            playerList.Add(new Player { Name = "Glenn Maxwell", TName = Player.Team.KXIP, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/KXIP/4.png", BasePrice = 10000000, Points = 90 });
            playerList.Add(new Player { Name = "Mitchell Johnson", TName = Player.Team.KXIP, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/KXIP/5.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Shaun Marsh", TName = Player.Team.KXIP, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/KXIP/6.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Anureet Singh", TName = Player.Team.KXIP, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/KXIP/7.png", BasePrice = 3000000, Points = 75 });
            playerList.Add(new Player { Name = "Gurkeraat Mann Singh", TName = Player.Team.KXIP, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/KXIP/8.png", BasePrice = 3000000, Points = 70 });
            playerList.Add(new Player { Name = "Hashim Amla", TName = Player.Team.KXIP, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/KXIP/9.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Farhaan Behardien", TName = Player.Team.KXIP, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/KXIP/10.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Rishi Dhawan", TName = Player.Team.KXIP, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/KXIP/11.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Kyle Abbott", TName = Player.Team.KXIP, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/KXIP/12.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Mohit Sharma", TName = Player.Team.KXIP, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/KXIP/13.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Sandeep Sharma", TName = Player.Team.KXIP, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/KXIP/14.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Axar Patel", TName = Player.Team.KXIP, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/KXIP/15.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Manan Vohra", TName = Player.Team.KXIP, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/KXIP/16.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "K C Cariappa", TName = Player.Team.KXIP, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/KXIP/17.png", BasePrice = 2000000, Points = 65 });
            playerList.Add(new Player { Name = "Umesh Yadav", TName = Player.Team.KKR, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/KKR/1.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Piyush Chawla", TName = Player.Team.KKR, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/KKR/2.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Gautam Gambhir", TName = Player.Team.KKR, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/KKR/3.png", BasePrice = 10000000, Points = 90 });
            playerList.Add(new Player { Name = "Jaidev Unadkat", TName = Player.Team.KKR, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/KKR/4.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Yusuf Pathan", TName = Player.Team.KKR, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/KKR/5.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Surya Kumar Yadav", TName = Player.Team.KKR, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/KKR/6.png", BasePrice = 5000000, Points = 80 });
            playerList.Add(new Player { Name = "Manish Pandey", TName = Player.Team.KKR, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/KKR/7.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Robin Uthappa", TName = Player.Team.KKR, CName = Player.Category.WK, Overseas = false, Image = "/Assets/KKR/8.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Morne Morkel", TName = Player.Team.KKR, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/KKR/9.png", BasePrice = 10000000, Points = 90 });
            playerList.Add(new Player { Name = "Andre Russell", TName = Player.Team.KKR, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/KKR/10.png", BasePrice = 10000000, Points = 90 });
            playerList.Add(new Player { Name = "Shakib Al Hasan", TName = Player.Team.KKR, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/KKR/11.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Chris Lynn", TName = Player.Team.KKR, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/KKR/12.png", BasePrice = 3000000, Points = 70 });
            playerList.Add(new Player { Name = "Sunil Narine", TName = Player.Team.KKR, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/KKR/13.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Kuldeep Yadav", TName = Player.Team.KKR, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/KKR/14.png", BasePrice = 3000000, Points = 70 });
            playerList.Add(new Player { Name = "Jason Holder", TName = Player.Team.KKR, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/KKR/15.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Brad Hogg", TName = Player.Team.KKR, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/KKR/16.png", BasePrice = 5000000, Points = 80 });
            playerList.Add(new Player { Name = "Ankit Rajpoot", TName = Player.Team.KKR, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/KKR/17.png", BasePrice = 2000000, Points = 65 });
            playerList.Add(new Player { Name = "Amit mishra", TName = Player.Team.DD, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/DD/1.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Pawan Negi", TName = Player.Team.DD, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/DD/2.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Shahbaz Nadeem", TName = Player.Team.DD, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/DD/3.png", BasePrice = 3000000, Points = 70 });
            playerList.Add(new Player { Name = "Mohammad Shami", TName = Player.Team.DD, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/DD/4.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Mayank Agarwal", TName = Player.Team.DD, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/DD/5.png", BasePrice = 3000000, Points = 75 });
            playerList.Add(new Player { Name = "Zaheer Khan", TName = Player.Team.DD, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/DD/6.png", BasePrice = 12500000, Points = 85 });
            playerList.Add(new Player { Name = "J P Duminy", TName = Player.Team.DD, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/DD/7.png", BasePrice = 15000000, Points = 90 });
            playerList.Add(new Player { Name = "Sanju Samson", TName = Player.Team.DD, CName = Player.Category.WK, Overseas = false, Image = "/Assets/DD/8.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Karun Nair", TName = Player.Team.DD, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/DD/9.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Quinton De Cock", TName = Player.Team.DD, CName = Player.Category.WK, Overseas = true, Image = "/Assets/DD/10.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Chris Morris", TName = Player.Team.DD, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/DD/11.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Nathan Coulternile", TName = Player.Team.DD, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/DD/12.png", BasePrice = 7500000, Points = 85 });
            playerList.Add(new Player { Name = "Imran Tahir", TName = Player.Team.DD, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/DD/13.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Shreyas Iyer", TName = Player.Team.DD, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/DD/14.png", BasePrice = 5000000, Points = 70 });
            playerList.Add(new Player { Name = "Carlos Braithwaite", TName = Player.Team.DD, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/DD/15.png", BasePrice = 3000000, Points = 70 });
            playerList.Add(new Player { Name = "Sam Billings", TName = Player.Team.DD, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/DD/16.png", BasePrice = 2000000, Points = 65 });
            playerList.Add(new Player { Name = "Rishabh Pant", TName = Player.Team.DD, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/DD/17.png", BasePrice = 2000000, Points = 65 });
            playerList.Add(new Player { Name = "M S Dhoni", TName = Player.Team.RPS, CName = Player.Category.WK, Overseas = false, Image = "/Assets/RPS/1.png", BasePrice = 20000000, Points = 95 });
            playerList.Add(new Player { Name = "Ravichandran Ashwin", TName = Player.Team.RPS, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/RPS/2.png", BasePrice = 15000000, Points = 85 });
            playerList.Add(new Player { Name = "Murugan Ashwin", TName = Player.Team.RPS, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/RPS/3.png", BasePrice = 7500000, Points = 75 });
            playerList.Add(new Player { Name = "Rajat Bhatia", TName = Player.Team.RPS, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/RPS/4.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Ashok Dinda", TName = Player.Team.RPS, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/RPS/5.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Faf Du Plessis", TName = Player.Team.RPS, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/RPS/6.png", BasePrice = 15000000, Points = 80 });
            playerList.Add(new Player { Name = "Mitchell Marsh", TName = Player.Team.RPS, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/RPS/7.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Albie Morkel", TName = Player.Team.RPS, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/RPS/8.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Irfan Pathan", TName = Player.Team.RPS, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/RPS/9.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Thisara Perara", TName = Player.Team.RPS, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/RPS/10.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Ajinky Rahane", TName = Player.Team.RPS, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/RPS/11.png", BasePrice = 15000000, Points = 85 });
            playerList.Add(new Player { Name = "Ishant Sharma", TName = Player.Team.RPS, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/RPS/12.png", BasePrice = 10000000, Points = 80 });
            playerList.Add(new Player { Name = "Steve Smith", TName = Player.Team.RPS, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/RPS/13.png", BasePrice = 15000000, Points = 90 });
            playerList.Add(new Player { Name = "Saurabh Tiwary", TName = Player.Team.RPS, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/RPS/14.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Adam Zampa", TName = Player.Team.RPS, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/RPS/15.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Usman Khawaja", TName = Player.Team.RPS, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/RPS/16.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Baba Aparajith", TName = Player.Team.RPS, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/RPS/17.png", BasePrice = 3000000, Points = 70 });
            playerList.Add(new Player { Name = "Virat Kohli", TName = Player.Team.RCB, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/RCB/1.png", BasePrice = 20000000, Points = 100 });
            playerList.Add(new Player { Name = "Varun Aaron", TName = Player.Team.RCB, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/RCB/2.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Sreenath Aravind", TName = Player.Team.RCB, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/RCB/3.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Stuart Binny", TName = Player.Team.RCB, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/RCB/4.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Yuzvendra Chahal", TName = Player.Team.RCB, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/RCB/5.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Ab De Villiers", TName = Player.Team.RCB, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/RCB/6.png", BasePrice = 20000000, Points = 95 });
            playerList.Add(new Player { Name = "Chris Gayle", TName = Player.Team.RCB, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/RCB/7.png", BasePrice = 20000000, Points = 95 });
            playerList.Add(new Player { Name = "Iqbal Abdulla", TName = Player.Team.RCB, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/RCB/8.png", BasePrice = 5000000, Points = 80 });
            playerList.Add(new Player { Name = "Sarfaraz Khan", TName = Player.Team.RCB, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/RCB/9.png", BasePrice = 3000000, Points = 75 });
            playerList.Add(new Player { Name = "K L Rahul", TName = Player.Team.RCB, CName = Player.Category.WK, Overseas = false, Image = "/Assets/RCB/10.png", BasePrice = 3000000, Points = 75 });
            playerList.Add(new Player { Name = "Parvez Rasool", TName = Player.Team.RCB, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/RCB/11.png", BasePrice = 3000000, Points = 70 });
            playerList.Add(new Player { Name = "Vinay Kumar", TName = Player.Team.RCB, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/RCB/12.png", BasePrice = 5000000, Points = 70 });
            playerList.Add(new Player { Name = "Mitchell Starc", TName = Player.Team.RCB, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/RCB/13.png", BasePrice = 20000000, Points = 90 });
            playerList.Add(new Player { Name = "Shane Watson", TName = Player.Team.RCB, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/RCB/14.png", BasePrice = 15000000, Points = 90 });
            playerList.Add(new Player { Name = "Chris Jordan", TName = Player.Team.RCB, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/RCB/15.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Harshal Patel", TName = Player.Team.RCB, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/RCB/16.png", BasePrice = 4000000, Points = 70 });
            playerList.Add(new Player { Name = "Rohit Sharma", TName = Player.Team.MI, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/MI/1.png", BasePrice = 20000000, Points = 95 });
            playerList.Add(new Player { Name = "Corey Anderson", TName = Player.Team.MI, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/MI/2.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Jasprit Bumrah", TName = Player.Team.MI, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/MI/3.png", BasePrice = 10000000, Points = 80 });
            playerList.Add(new Player { Name = "Jos Buttler", TName = Player.Team.MI, CName = Player.Category.WK, Overseas = true, Image = "/Assets/MI/4.png", BasePrice = 10000000, Points = 85 });
            playerList.Add(new Player { Name = "Unmukt Chand", TName = Player.Team.MI, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/MI/5.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Merchant De Lange", TName = Player.Team.MI, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/MI/6.png", BasePrice = 3000000, Points = 75 });
            playerList.Add(new Player { Name = "Shreyas Gopal", TName = Player.Team.MI, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/MI/7.png", BasePrice = 2000000, Points = 70 });
            playerList.Add(new Player { Name = "Harbhajan Singh", TName = Player.Team.MI, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/MI/8.png", BasePrice = 15000000, Points = 85 });
            playerList.Add(new Player { Name = "Sidhesh Lad", TName = Player.Team.MI, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/MI/9.png", BasePrice = 1000000, Points = 70 });
            playerList.Add(new Player { Name = "Mitchell Mclenaghan", TName = Player.Team.MI, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/MI/10.png", BasePrice = 10000000, Points = 80 });
            playerList.Add(new Player { Name = "Hardhik Pandya", TName = Player.Team.MI, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/MI/11.png", BasePrice = 10000000, Points = 75 });
            playerList.Add(new Player { Name = "Krunal Pandya ", TName = Player.Team.MI, CName = Player.Category.ALL, Overseas = false, Image = "/Assets/MI/12.png", BasePrice = 5000000, Points = 70 });
            playerList.Add(new Player { Name = "Parthiv Patel", TName = Player.Team.MI, CName = Player.Category.WK, Overseas = false, Image = "/Assets/MI/13.png", BasePrice = 7500000, Points = 75 });
            playerList.Add(new Player { Name = "Kieron Pollard", TName = Player.Team.MI, CName = Player.Category.ALL, Overseas = true, Image = "/Assets/MI/14.png", BasePrice = 15000000, Points = 85 });
            playerList.Add(new Player { Name = "Nitish Rana", TName = Player.Team.MI, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/MI/15.png", BasePrice = 2000000, Points = 70 });
            playerList.Add(new Player { Name = "Ambati Rayudu", TName = Player.Team.MI, CName = Player.Category.BAT, Overseas = false, Image = "/Assets/MI/16.png", BasePrice = 10000000, Points = 80 });
            playerList.Add(new Player { Name = "Lendl Simmons", TName = Player.Team.MI, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/MI/17.png", BasePrice = 10000000, Points = 80 });
            playerList.Add(new Player { Name = "Tim Southee", TName = Player.Team.MI, CName = Player.Category.BOWL, Overseas = true, Image = "/Assets/MI/18.png", BasePrice = 7500000, Points = 80 });
            playerList.Add(new Player { Name = "Vinay Kumar", TName = Player.Team.MI, CName = Player.Category.BOWL, Overseas = false, Image = "/Assets/MI/19.png", BasePrice = 5000000, Points = 75 });
            playerList.Add(new Player { Name = "Martin Guptill", TName = Player.Team.MI, CName = Player.Category.BAT, Overseas = true, Image = "/Assets/MI/20.png", BasePrice = 10000000, Points = 80 });

            //shuffling the list and binding to grid
            Random r = new Random();
            int randomIndex = 0;
            while (randomPlayerList.Count < 96)
            {
                 randomIndex = r.Next(0, playerList.Count);
                //Choose a random object in the list
                randomPlayerList.Add(playerList[randomIndex]); //add it to the new, random list
                playerList.RemoveAt(randomIndex); //remove to avoid duplicates
            }
            PlayerList.ItemsSource = randomPlayerList;
        }
        
        

        public PivotPage()
        {
            this.InitializeComponent();
            binding();               
        }
    }
}
