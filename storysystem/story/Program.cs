//////////////////////DATAS////////////////////////////////////
using System.ComponentModel;
using System.Data.Common;

/// Burada büyün hesap sistemleri mövcuddur. 
/// Hesaba giriş ve yeni hesab yaratmaq fonksiyonu var. 

List<string> adlist = new List<string>();
List<string> sifrelist = new List<string>();
List<string> guvenliklist = new List<string>();
List<string> adminlist = new List<string>();

string addata = "addata.txt";
string sifredata = "sifredata.txt";
string guvenlikdata = "guvenlikdata.txt";
string admindata = "admindata.txt";

if (File.Exists(addata))
{
    adlist.AddRange(File.ReadAllLines(addata));
}
if (File.Exists(sifredata))
{
    sifrelist.AddRange(File.ReadAllLines(sifredata));
}
if (File.Exists(guvenlikdata))
{
    guvenliklist.AddRange(File.ReadAllLines(guvenlikdata));
}
if (File.Exists(admindata))
{
    adminlist.AddRange(File.ReadAllLines(admindata));
}

//////////////Account Register/////////////////////////
int anket = 0;
if (adminlist.Count > 0)
{
    Console.WriteLine("1-Yeni Hesap \n2-Giris");
    while (true)
    {
        anket = Convert.ToInt32(Console.ReadLine());
        if (anket == 1 || anket == 2)
        {
            break;
        }
        else
        {
            Console.WriteLine("Sehv giris!");
        }
    }

}


if (anket == 1 || adminlist.Count == 0)
{

    Console.WriteLine("Yeni hesab acırsınız!");
    try
    {
        Console.Write("Ad girin: ");
        string regisim = Console.ReadLine();
        adlist.Add(regisim);
        File.AppendAllText(addata, regisim + Environment.NewLine);

        Console.Write("Parol girin: ");
        string regparol = Console.ReadLine();
        sifrelist.Add(regparol);
        File.AppendAllText(sifredata, regparol + Environment.NewLine);

        Console.Write("Admin hesabidir?(Y/N) ");
        while (true)
        {
            string adminreg = Console.ReadLine().ToLower();
            if (adminreg == "y" || adminreg == "n")
            {
                adminlist.Add(adminreg);
                File.AppendAllText(admindata, adminreg + Environment.NewLine);
                break;
            }
            else
            {
                Console.WriteLine("Sehv giris!");
            }
        }

        Console.Write("Hansi mektebde oxumusan? ");
        string mekteb = Console.ReadLine();
        guvenliklist.Add(mekteb);
        File.AppendAllText(guvenlikdata, mekteb + Environment.NewLine);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Xeta: " + ex);
    }
    Console.WriteLine("Ugurla yeni hesab acildi!");
    anket = 2;
}


////////////////////Account Login///////////////////////

string isimgiris = "";
string sifregiris = "";

while (anket == 2)
{
    int denemehak = 2;
    while (true)
    {
        Console.WriteLine("Ad girisinde ilk 3 defe uğursuz olarsa proqram avtomatik bağlanacaq!");
        Console.Write("Adınızı girin: ");
        isimgiris = Console.ReadLine();
        if (!adlist.Contains(isimgiris) && denemehak > 0)
        {
            Console.WriteLine("Sehv giris!");
            denemehak--;
        }
        else if (denemehak == 0)
        {
            Environment.Exit(0);
        }
        else
        {
            break;
        }
    }
    denemehak = 2;
    while (true)
    {
        Console.Write("Parolunuzu girin: ");
        sifregiris = Console.ReadLine();
        if (!sifrelist.Contains(sifregiris) && denemehak > 0)
        {
            Console.WriteLine("Sehv giris!");
            denemehak--;
        }
        else if (denemehak == 0)
        {
            Console.Write("Hansi mektebi bitirmisen? ");
            string mekteb = Console.ReadLine();
            if (!guvenliklist.Contains(mekteb))
            {
                Console.WriteLine("Sehv giris! Proqram baglanir!");
                Environment.Exit(0);
            }
            else
            {
                Console.Write("Yeni şifrəni daxil edin: ");
                string newPassword = Console.ReadLine();

                int index = 0;
                while (index < adlist.Count)
                {
                    if (isimgiris == adlist[index])
                    {
                        break;
                    }
                    index++;
                }
                sifrelist[index] = newPassword;
                File.WriteAllLines(sifredata, sifrelist);
                Console.WriteLine("Ugurla sifre yenilendi!");
            }
        }
        else
        {
            break;
        }
    }
    break;
}

//--------------------------------------------------//
//////////////////////SYSTEMS ADMİN////////////////////////////
//--------------------------------------------------//

//Admine aid olan bütün sistemlər buradadır. Hər hansı bir inkişafda burada etmək daha asand olacaq.

List<string> mehsullist = new List<string>();
List<double> qiymetlist = new List<double>();

string mehsuldata = "mehsuldata.txt";
string qiymetdata = "qiymetdata.txt";

if (File.Exists(mehsuldata))
{
    mehsullist.AddRange(File.ReadAllLines(mehsuldata));
}
if (File.Exists(qiymetdata))
{
    qiymetlist.AddRange(File.ReadAllLines(qiymetdata).Select(x => double.Parse(x)));
}

void mehsulbax()   ///Eyni zamanda user system də sayılmaqdadır.
{
    Console.WriteLine("------MehsulList------");
    if (mehsullist.Count > 0)
    {
        Console.WriteLine("ID-MEHSUL-Qiymet");

    }
    else
    {
        Console.WriteLine("Mehsul yoxdur!");
    }
    for (int i = 0; i < mehsullist.Count; i++)
    {
        Console.WriteLine($"{i + 1}-{mehsullist[i]}-{qiymetlist[i]}");
    }
}

void mehsulelave()
{
    Console.WriteLine("------Mehsul Elave Et------");
    try
    {
        while (true)
        {
            Console.Write("Mehsul adi: ");
            string adelave = Console.ReadLine();
            if (adelave.Length > 1)
            {
                mehsullist.Add(adelave);
                File.AppendAllText(mehsuldata, adelave + Environment.NewLine);
                break;
            }
            else
            {
                Console.WriteLine("En az 2 herfli olmalidir!");
            }
        }
        while (true)
        {
            Console.Write("Mehsul Qiymeti: ");
            double qiymetelave = Convert.ToDouble(Console.ReadLine().Replace('.', ','));
            if (qiymetelave > 0)
            {
                qiymetlist.Add(qiymetelave);
                File.AppendAllText(qiymetdata, qiymetelave.ToString() + Environment.NewLine);
                break;
            }
            else
            {
                Console.WriteLine("Qiymet 0-dan ferqli olmalidir!");
            }

        }

    }
    catch (Exception ex)
    {
        Console.WriteLine("Xeta: " + ex);
    }
}

void mehsulsil()
{
    Console.WriteLine("------Mehsul Sil------");
    if (mehsullist.Count > 0)
    {
        Console.WriteLine("ID-MEHSUL-Qiymet");
    }
    else
    {
        Console.WriteLine("Mehsul yoxdur!");
    }
    for (int i = 0; i < mehsullist.Count; i++)
    {
        Console.WriteLine($"{i + 1}-{mehsullist[i]}-{qiymetlist[i]}");

    }
    Console.WriteLine("------------------------");

    if (mehsullist.Count > 0)
    {
        try
        {
            Console.WriteLine("Silmek istediginiz mehsul ID: ");
            int silid = Convert.ToInt32(Console.ReadLine());
            if (silid > 0 && silid <= mehsullist.Count)
            {
                mehsullist.RemoveAt(silid - 1);
                File.WriteAllLines(mehsuldata, mehsullist);

                qiymetlist.RemoveAt(silid - 1);
                File.WriteAllLines(qiymetdata, qiymetlist.Select(x => x.ToString()));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Xeta: " + ex);
        }
        Console.WriteLine("Ugurla silindi!");
    }

}

void mehsulqiymetdeyisdirme()
{
    Console.WriteLine("------Mehsul Qiymet Deyisdirme------");
    if (mehsullist.Count > 0)
    {
        Console.WriteLine("ID-MEHSUL-Qiymet");
    }
    else
    {
        Console.WriteLine("Mehsul yoxdur!");
    }
    for (int i = 0; i < mehsullist.Count; i++)
    {
        Console.WriteLine($"{i + 1}-{mehsullist[i]}-{qiymetlist[i]}");

    }
    Console.WriteLine("------------------------");

    if (mehsullist.Count > 0)
    {
        try
        {
            int mehsulid = 0;
            double qiymet = 0;
            while (true)
            {
                Console.Write("Qiymetini deyisdirmek istediginiz mehsul ID: ");
                mehsulid = Convert.ToInt32(Console.ReadLine());
                if (mehsulid > 0 && mehsulid <= mehsullist.Count)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Sehv ID!");
                }
            }
            while (true)
            {
                Console.Write("Yeni qiymet: ");
                qiymet = Convert.ToDouble(Console.ReadLine());
                if (qiymet > 0)
                {
                    qiymetlist[mehsulid - 1] = qiymet;
                    File.WriteAllLines(qiymetdata, qiymetlist.Select(x => x.ToString()));
                    break;
                }
                else
                {
                    Console.WriteLine("0-dan böyük olmalıdır!");
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Xeta: " + ex);
        }
        Console.WriteLine("Ugurla silindi!");
    }
}

void istifadeciler()
{
    Console.WriteLine("------İstifadeciler------");
    Console.WriteLine("ID-AD-ADMIN");
    for (int i = 0; i < adlist.Count; i++)
    {
        Console.WriteLine($"{i + 1}-{adlist[i]}-{adminlist[i]}");
    }
}



//--------------------------------------------------//
//////////////////////USER SYSTEM/////////////////////
//--------------------------------------------------//

//User ə aid olan bütün sistemlər buradadır. 

List<string> sifarislist = new List<string>();
List<string> saylist = new List<string>(); // Mehsuldan nece dene sifaris verildigi sayi
double balans = 0;

string sifarisdata = "sifarisdata.txt";
string saydata = "saydata.txt";
string balansdata = "balansdata.txt";

if (File.Exists(balansdata))
{
    balans = Convert.ToDouble(File.ReadAllText(balansdata));
}
if (File.Exists(sifarisdata))
{
    sifarislist.AddRange(File.ReadAllLines(sifarisdata));
}
if (File.Exists(saydata))
{
    saylist.AddRange(File.ReadAllLines(saydata));
}

///-----
void sifarisler()    ///Admine bagli sistemdir. Sifaris datalarini cekmek üçün burada yazılır.
{
    Console.WriteLine("------SİFARİŞLER------");
    if (sifarislist.Count > 0)
    {
        Console.WriteLine("ID-SİFARİŞ-SAY");
        for (int i = 0; i < sifarislist.Count; i++)
        {
            Console.WriteLine($"{i + 1}-{sifarislist[i]}-{saylist[i]}");
        }
    }
    else
    {
        Console.WriteLine("Sifariş yoxdur!");
    }
}
///-----

void axtaris()
{
    Console.WriteLine("------AXTARIŞ------");
    if (mehsullist.Count == 0)
    {
        Console.WriteLine("Axtarmağa mehsul yoxdur!");
        return;
    }
     
    Console.WriteLine("Sistemden çıxmaq üçün 0 yazın!");
    string axtarisad = "";
    bool tapildi = false;
    try
    {
      while (true)
      {
        Console.Write("Axtarmaq istediginiz mehsul adı: ");
        axtarisad = Console.ReadLine().ToLower();
        if (axtarisad == "0")
        {
            break;
        }

        for (int i = 0; i < mehsullist.Count; i++)
        {
            if (mehsullist[i].ToLower() == axtarisad)
            {
                Console.WriteLine($"{i + 1}-{mehsullist[i]}-{qiymetlist[i]}");
                tapildi = true;
            }
        }

        if (!tapildi)
        {
            Console.WriteLine("Axtardiginiz mehsul yoxdur. Yeniden axtarin");
        }
      }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Xeta: " + ex);
     }

}

void balansbax()
{
    Console.WriteLine("------BALANS------");
    Console.WriteLine($"Balans: {balans}");
}

void balansyukle()
{
    Console.WriteLine("------BALANS YÜKLE------");
    Console.WriteLine($"Balans: {balans}");
    int balansyukle = 0;

    while (true)
    {
        Console.Write("Yüklemek istediğiniz balans(Tam Eded): ");
        try
        {
            balansyukle = Convert.ToInt32(Console.ReadLine());
           if (balansyukle <= 0)
            {
                Console.WriteLine("0-dan çox olmalıdır!");
            }
           else
           {
                balans += balansyukle;
                File.WriteAllText(balansdata, balans.ToString());
           break;
          }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Xeta: " + ex);
        }
    }
}

void mehsulsatinal()
{
    Console.WriteLine("------Mehsul SATIN AL------");
    if (mehsullist.Count > 0)
    {
        Console.WriteLine("ID-MEHSUL-Qiymet");
    }
    else
    {
        Console.WriteLine("Mehsul yoxdur!");
    }
    for (int i = 0; i < mehsullist.Count; i++)
    {
        Console.WriteLine($"{i + 1}-{mehsullist[i]}-{qiymetlist[i]}");
    }
    Console.WriteLine("---------------------------");

    int al = 0;
    while (true)
    {
        Console.WriteLine("---------------------------");
        Console.WriteLine("Cixmaq üçün 0 yazin");
        Console.WriteLine("Balans: " + balans);
        Console.Write("Almaq istediyiniz mehsul id:");
        try
        {
            al = Convert.ToInt32(Console.ReadLine());
            if (al == 0)
            {
                break;
            }

            if (al > 0 && al <= mehsullist.Count)
            {
                if (balans >= qiymetlist[al - 1])
                {
                    balans -= qiymetlist[al - 1];
                    File.WriteAllText(balansdata, balans.ToString());

                    sifarislist.Add(mehsullist[al - 1]);
                    saylist.Add("1");

                    File.WriteAllLines(sifarisdata, sifarislist);
                    File.WriteAllLines(saydata, saylist);
                }
                else
                {
                    Console.WriteLine("Balans catmir!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Xeta: " + ex);
        }
    }
}





//--------------------------------------------------//
//////////////////////MENU////////////////////////////
//--------------------------------------------------//


int adminindex = adlist.IndexOf(isimgiris);
if (adminlist[adminindex] == "y")   //Eger admin hesabidirsa
{
    do
    {
        Console.WriteLine("------ADMIN MENU------");
        Console.WriteLine("1-Mehsullar \n2-Yeni Mehsul Elave Et \n3-Mehsul sil \n4-Mehsul qiymet deyisdirme \n5-Sifarişler \n6-İstifadecileri görüntüle \n7-Cixis");
        int adminsecme = 0;
        try
        {
            while (true)
            {
                Console.Write("ID: ");
                adminsecme = Convert.ToInt32(Console.ReadLine());
                if (adminsecme < 0 || adminsecme > 7)
                {
                    Console.WriteLine("Sehv giris!");
                }
                else
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Xeta: " + ex);
        }


        if (adminsecme == 1)
        {
            mehsulbax();
        }
        else if (adminsecme == 2)
        {
            mehsulelave();
        }
        else if (adminsecme == 3)
        {
            mehsulsil();
        }
        else if (adminsecme == 4)
        {
            mehsulqiymetdeyisdirme();
        }
        else if (adminsecme == 5)
        {
            sifarisler();
        }
        else if (adminsecme == 6)
        {
            istifadeciler();
        }
        else if (adminsecme == 7)
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Sehv ID!");
        }


    } while (true);

}

else  //Eger istifadeci hesabidirsa
{
    do
    {
        Console.WriteLine("------ISTIFADECI MENU------");
        Console.WriteLine("1-Mehsullar \n2-Axtarış \n3-Balans \n4-Balans yükle \n5-Mehsul Satın Al \n6-Cixis");
        int istsecme = 0;
        try
        {
            while (true)
            {
                Console.Write("ID: ");
                istsecme = Convert.ToInt32(Console.ReadLine());
                if (istsecme < 0 || istsecme > 6)
                {
                    Console.WriteLine("Sehv giris!");
                }
                else
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Xeta: " + ex);
        }
        
        
        if (istsecme == 1)
        {
            mehsulbax();
        }
        else if (istsecme == 2)
        {
            axtaris();
        }
        else if (istsecme == 3)
        {
            balansbax();
        }
        else if (istsecme == 4)
        {
            balansyukle();
        }
        else if (istsecme == 5)
        {
            mehsulsatinal();
        }
        else if (istsecme == 6)
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Sehv ID!");
        }
        

    } while (true);

}




