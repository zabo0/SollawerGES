using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SollawerGES.Utils
{
    public static class Configurations
    {
        private static string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static string filePath = Path.Combine(projectDirectory, @"..\..\Assets\Configurations.txt");

        public static bool Animation;
        public static int AnimationDelayTime;

        public static double SnapStep;

        public static double WidthPanel;
        public static double HeightPanel;
        public static double SpacePanel;
        public static int CountPanel;

        public static double WidthZAsik;
        public static double HeightZAsik;

        public static double WidthWAsik;
        public static double HeightWAsik;

        public static double HeightProfil;
        public static double SpaceProfil;

        public static double WidthAksBirl;
        public static double HeightAksBirl;

        public static double WidthMafsal;
        public static double HeightMafsal;
        public static double ActuatorDistanceMafsal;

        public static double WidthDirek;
        public static double HeightDirek;

        
        public static void readConfiguration()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader(filePath);
                line = sr.ReadLine();
                while (line != null)
                {
                    setConfigs(line);
                    line = sr.ReadLine();
                }
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        public static void writeConfigurations()
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath);

                string line = "";
                if (Animation)
                {
                    line += "<Animation><!true>" + Environment.NewLine;
                }
                else
                {
                    line += "<Animation><!false>" + Environment.NewLine;
                }

                line += "<AnimationDelayTime><!" + AnimationDelayTime.ToString() + ">;" + Environment.NewLine;
                line += "<SnapStep><!" + SnapStep.ToString() + ">;" + Environment.NewLine;
                line += "<Panel><!" + WidthPanel.ToString() + "><!" + HeightPanel + "><!" + SpacePanel + "><!" + CountPanel +">;" + Environment.NewLine;
                line += "<ZProfilAsik><!" + WidthZAsik.ToString() + "><!" + HeightZAsik + ">;" + Environment.NewLine;
                line += "<WProfilAsik><!" + WidthWAsik.ToString() + "><!" + HeightWAsik + ">;" + Environment.NewLine;
                line += "<Profil><!" + HeightProfil.ToString() + "><!" + SpaceProfil  + ">;" + Environment.NewLine;
                line += "<AksBirlestirici><!" + WidthAksBirl.ToString() + "><!" + HeightAksBirl  + ">;" + Environment.NewLine;
                line += "<Mafsal><!" + WidthMafsal.ToString() + "><!" + HeightMafsal + "><!" + ActuatorDistanceMafsal + ">;" + Environment.NewLine;
                line += "<Direk><!" + WidthDirek.ToString() + "><!" + HeightDirek  + ">;" + Environment.NewLine;

                sw.WriteLine(line);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        private static void setConfigs(string line)
        {
            string pattern = @"<(.+?)>";

            Match match = Regex.Match(line, pattern);

            if (match.Success)
            {
                string result = match.Groups[1].Value;

                switch (result)
                {
                    case "Animation":
                        {
                            MatchCollection childMatches = Regex.Matches(line, @"<!(.+?)>");
                            if (childMatches.Count > 0)
                            {
                                if (childMatches[0].Groups[1].Value.Equals("true"))
                                {
                                    Animation = true;
                                }
                                else if (childMatches[0].Groups[1].Value.Equals("false"))
                                {
                                    Animation = false;
                                }
                            }
                            break;
                        }
                    case "AnimationDelayTime":
                        {
                            MatchCollection childMatches = Regex.Matches(line, @"<!(.+?)>");
                            if (childMatches.Count > 0)
                            {
                                AnimationDelayTime = int.Parse(childMatches[0].Groups[1].Value);
                            }
                            break ;
                        }
                    case "SnapStep":
                        {
                            MatchCollection childMatches = Regex.Matches(line, @"<!(.+?)>");
                            if (childMatches.Count > 0)
                            {
                                SnapStep = int.Parse(childMatches[0].Groups[1].Value);
                            }
                            break;
                        }
                    case "Panel":
                        {
                            MatchCollection childMatches = Regex.Matches(line, @"<!(.+?)>");
                            if (childMatches.Count > 0)
                            {
                                WidthPanel = double.Parse(childMatches[0].Groups[1].Value);
                                HeightPanel = double.Parse(childMatches[1].Groups[1].Value);
                                SpacePanel = double.Parse(childMatches[2].Groups[1].Value);
                                CountPanel = int.Parse(childMatches[3].Groups[1].Value);
                            }
                            break;
                        }
                    case "ZProfilAsik":
                        {
                            MatchCollection childMatches = Regex.Matches(line, @"<!(.+?)>");
                            if (childMatches.Count > 0)
                            {
                                WidthZAsik = double.Parse(childMatches[0].Groups[1].Value);
                                HeightZAsik = double.Parse(childMatches[1].Groups[1].Value);
                            }
                            break;
                        }
                    case "WProfilAsik":
                        {
                            MatchCollection childMatches = Regex.Matches(line, @"<!(.+?)>");
                            if (childMatches.Count > 0)
                            {
                                WidthWAsik = double.Parse(childMatches[0].Groups[1].Value);
                                HeightWAsik = double.Parse(childMatches[1].Groups[1].Value);
                            }
                            break;
                        }
                    case "Profil":
                        {
                            MatchCollection childMatches = Regex.Matches(line, @"<!(.+?)>");
                            if (childMatches.Count > 0)
                            {
                                HeightProfil = double.Parse(childMatches[0].Groups[1].Value);
                                SpaceProfil = double.Parse(childMatches[1].Groups[1].Value);
                            }
                            break;
                        }
                    case "AksBirlestirici":
                        {
                            MatchCollection childMatches = Regex.Matches(line, @"<!(.+?)>");
                            if (childMatches.Count > 0)
                            {
                                WidthAksBirl = double.Parse(childMatches[0].Groups[1].Value);
                                HeightAksBirl = double.Parse(childMatches[1].Groups[1].Value);
                            }
                            break;
                        }
                    case "Mafsal":
                        {
                            MatchCollection childMatches = Regex.Matches(line, @"<!(.+?)>");
                            if (childMatches.Count > 0)
                            {
                                WidthMafsal = double.Parse(childMatches[0].Groups[1].Value);
                                HeightMafsal = double.Parse(childMatches[1].Groups[1].Value);
                                ActuatorDistanceMafsal = double.Parse(childMatches[2].Groups[1].Value);
                            }
                            break;
                        }
                    case "Direk":
                        {
                            MatchCollection childMatches = Regex.Matches(line, @"<!(.+?)>");
                            if (childMatches.Count > 0)
                            {
                                WidthDirek = double.Parse(childMatches[0].Groups[1].Value);
                                HeightDirek = double.Parse(childMatches[1].Groups[1].Value);
                            }
                            break;
                        }
                }
            }



        }
    }
}
