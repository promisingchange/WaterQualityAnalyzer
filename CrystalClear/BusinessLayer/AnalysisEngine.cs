using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrystalClear.DL;
using CrystalClear.BL.Entities;

namespace CrystalClear.BL
{
    public class AnalysisEngine
    {
        public static SimpleIndices CalculateSimpleIndices(double Temperature, double Cph, double Cdo, double Cbod5, double Ccod, double Cnh4n, double Cno2n, double Cno3n, double Css, double Ccl, double Ccb)
        {
            var db = CrystalClearDB.GetInstance();
            if (db == null) return null;

            var simpleIndices = new SimpleIndices();

            // Calculate simple indices
            // PH
            var avgEnvironmentalStandard = db.GetAverageEnvironmentalStandard();
            var SphAvg = (avgEnvironmentalStandard.PH2 + avgEnvironmentalStandard.PH1) / 2.0;
            var Sph = avgEnvironmentalStandard.PH1;
            simpleIndices.Iph = (Cph - SphAvg) / (Sph - SphAvg);

            // DO
            var oxygenDO = db.GetOxygenDOForTemperature(Temperature);
            var Sdo = avgEnvironmentalStandard.DO;
            simpleIndices.Ido = (oxygenDO.DO - Cdo) / (oxygenDO.DO - Sdo);

            // BOD5
            simpleIndices.Ibod5 = Cbod5 / avgEnvironmentalStandard.BOD5;

            // COD
            simpleIndices.Icod = Ccod / avgEnvironmentalStandard.COD;

            // NH4-N
            simpleIndices.Inh4n = Cnh4n / avgEnvironmentalStandard.NH4N;

            // NO2-N
            simpleIndices.Ino2n = Cno2n / avgEnvironmentalStandard.NO2N;

            // NO3-N
            simpleIndices.Ino3n = Cno3n / avgEnvironmentalStandard.NO3N;

            // SS
            simpleIndices.Iss = Css / avgEnvironmentalStandard.SS;

            // CL
            simpleIndices.Icl = Ccl / avgEnvironmentalStandard.CL;

            // CB
            simpleIndices.Icb = Ccb / avgEnvironmentalStandard.CB;

            return simpleIndices;
        }
        public static WeightCoefficients GetWeightCoefficients(double Temperature, SimpleIndices CalculatedIndices)
        {
            var db = CrystalClearDB.GetInstance();
            if (db == null) return null;

            var wks = new WeightCoefficients();

            var individualIndices = db.GetIndividualIndicesForTemperature(Temperature);
            var weightCoefficients = db.GetWeightCoefficientsForTemperature(Temperature);

            // Get individual Weight Coefficient based on the Individual Indices, for calculated simple indices
            // PH
            var i = 2; // index number, for Level 3
            var Iph = System.Math.Round(CalculatedIndices.Iph, 5, MidpointRounding.AwayFromZero);
            while (0 <= i && i <= 4)
            {
                if (Iph <= individualIndices[i].PH)
                {
                    if (i != 0)
                    {
                        if (individualIndices[i - 1].PH < Iph)
                        {
                            // Found a relevant Level
                            break;
                        }

                        i--;
                    }
                    else
                    {
                        // reached to Level 1
                        // Found
                        break;
                    }
                }
                else
                {
                    if (i != 4)
                    {
                        if (Iph < individualIndices[i + 1].PH)
                        {
                            // Found a relevant Level
                            i += 1;
                            break;
                        }

                        i++;
                    }
                    else
                    {
                        // reached to Level 5
                        // Found
                        break;
                    }
                }
            }

            wks.PH = weightCoefficients[i].PH;

            // DO
            i = 2; // index number, for Level 3
            var Ido = System.Math.Round(CalculatedIndices.Ido, 5, MidpointRounding.AwayFromZero);
            while (0 <= i && i <= 4)
            {
                if (Ido <= individualIndices[i].DO)
                {
                    if (i != 0)
                    {
                        if (individualIndices[i - 1].DO < Ido)
                        {
                            // Found a relevant Level
                            break;
                        }

                        i--;
                    }
                    else
                    {
                        // reached to Level 1
                        // Found
                        break;
                    }
                }
                else
                {
                    if (i != 4)
                    {
                        if (Ido < individualIndices[i + 1].DO)
                        {
                            // Found a relevant Level
                            i += 1;
                            break;
                        }

                        i++;
                    }
                    else
                    {
                        // reached to Level 5
                        // Found
                        break;
                    }
                }
            }

            wks.DO = weightCoefficients[i].DO;

            // BOD5
            i = 2; // index number, for Level 3
            var Ibod5 = System.Math.Round(CalculatedIndices.Ibod5, 5, MidpointRounding.AwayFromZero);
            while (0 <= i && i <= 4)
            {
                if (Ibod5 <= individualIndices[i].BOD5)
                {
                    if (i != 0)
                    {
                        if (individualIndices[i - 1].BOD5 < Ibod5)
                        {
                            // Found a relevant Level
                            break;
                        }

                        i--;
                    }
                    else
                    {
                        // reached to Level 1
                        // Found
                        break;
                    }
                }
                else
                {
                    if (i != 4)
                    {
                        if (Ibod5 < individualIndices[i + 1].BOD5)
                        {
                            // Found a relevant Level
                            i += 1;
                            break;
                        }

                        i++;
                    }
                    else
                    {
                        // reached to Level 5
                        // Found
                        break;
                    }
                }
            }

            wks.BOD5 = weightCoefficients[i].BOD5;

            // COD
            i = 2; // index number, for Level 3
            var Icod = System.Math.Round(CalculatedIndices.Icod, 5, MidpointRounding.AwayFromZero);
            while (0 <= i && i <= 4)
            {
                if (Icod <= individualIndices[i].COD)
                {
                    if (i != 0)
                    {
                        if (individualIndices[i - 1].COD < Icod)
                        {
                            // Found a relevant Level
                            break;
                        }

                        i--;
                    }
                    else
                    {
                        // reached to Level 1
                        // Found
                        break;
                    }
                }
                else
                {
                    if (i != 4)
                    {
                        if (Icod < individualIndices[i + 1].COD)
                        {
                            // Found a relevant Level
                            i += 1;
                            break;
                        }

                        i++;
                    }
                    else
                    {
                        // reached to Level 5
                        // Found
                        break;
                    }
                }
            }

            wks.COD = weightCoefficients[i].COD;

            // NH4-N
            i = 2; // index number, for Level 3
            var Inh4n = System.Math.Round(CalculatedIndices.Inh4n, 5, MidpointRounding.AwayFromZero);
            while (0 <= i && i <= 4)
            {
                if (Inh4n <= individualIndices[i].NH4N)
                {
                    if (i != 0)
                    {
                        if (individualIndices[i - 1].NH4N < Inh4n)
                        {
                            // Found a relevant Level
                            break;
                        }

                        i--;
                    }
                    else
                    {
                        // reached to Level 1
                        // Found
                        break;
                    }
                }
                else
                {
                    if (i != 4)
                    {
                        if (Inh4n < individualIndices[i + 1].NH4N)
                        {
                            // Found a relevant Level
                            i += 1;
                            break;
                        }

                        i++;
                    }
                    else
                    {
                        // reached to Level 5
                        // Found
                        break;
                    }
                }
            }

            wks.NH4N = weightCoefficients[i].NH4N;

            // NO2-N
            i = 2; // index number, for Level 3
            var Ino2n = System.Math.Round(CalculatedIndices.Ino2n, 5, MidpointRounding.AwayFromZero);
            while (0 <= i && i <= 4)
            {
                if (Ino2n <= individualIndices[i].NO2N)
                {
                    if (i != 0)
                    {
                        if (individualIndices[i - 1].NO2N < Ino2n)
                        {
                            // Found a relevant Level
                            break;
                        }

                        i--;
                    }
                    else
                    {
                        // reached to Level 1
                        // Found
                        break;
                    }
                }
                else
                {
                    if (i != 4)
                    {
                        if (Ino2n < individualIndices[i + 1].NO2N)
                        {
                            // Found a relevant Level
                            i += 1;
                            break;
                        }

                        i++;
                    }
                    else
                    {
                        // reached to Level 5
                        // Found
                        break;
                    }
                }
            }

            wks.NO2N = weightCoefficients[i].NO2N;

            // NO3-N
            i = 2; // index number, for Level 3
            var Ino3n = System.Math.Round(CalculatedIndices.Ino3n, 5, MidpointRounding.AwayFromZero);
            while (0 <= i && i <= 4)
            {
                if (Ino2n <= individualIndices[i].NO3N)
                {
                    if (i != 0)
                    {
                        if (individualIndices[i - 1].NO3N < Ino3n)
                        {
                            // Found a relevant Level
                            break;
                        }

                        i--;
                    }
                    else
                    {
                        // reached to Level 1
                        // Found
                        break;
                    }
                }
                else
                {
                    if (i != 4)
                    {
                        if (Ino3n < individualIndices[i + 1].NO3N)
                        {
                            // Found a relevant Level
                            i += 1;
                            break;
                        }

                        i++;
                    }
                    else
                    {
                        // reached to Level 5
                        // Found
                        break;
                    }
                }
            }

            wks.NO3N = weightCoefficients[i].NO3N;

            // SS
            i = 2; // index number, for Level 3
            var Iss = System.Math.Round(CalculatedIndices.Iss, 5, MidpointRounding.AwayFromZero);
            while (0 <= i && i <= 4)
            {
                if (Iss <= individualIndices[i].SS)
                {
                    if (i != 0)
                    {
                        if (individualIndices[i - 1].SS < Iss)
                        {
                            // Found a relevant Level
                            break;
                        }

                        i--;
                    }
                    else
                    {
                        // reached to Level 1
                        // Found
                        break;
                    }
                }
                else
                {
                    if (i != 4)
                    {
                        if (Iss < individualIndices[i + 1].SS)
                        {
                            // Found a relevant Level
                            i += 1;
                            break;
                        }

                        i++;
                    }
                    else
                    {
                        // reached to Level 5
                        // Found
                        break;
                    }
                }
            }

            wks.SS = weightCoefficients[i].SS;

            // CL
            i = 2; // index number, for Level 3
            var Icl = System.Math.Round(CalculatedIndices.Icl, 5, MidpointRounding.AwayFromZero);
            while (0 <= i && i <= 4)
            {
                if (Icl <= individualIndices[i].CL)
                {
                    if (i != 0)
                    {
                        if (individualIndices[i - 1].CL < Icl)
                        {
                            // Found a relevant Level
                            break;
                        }

                        i--;
                    }
                    else
                    {
                        // reached to Level 1
                        // Found
                        break;
                    }
                }
                else
                {
                    if (i != 4)
                    {
                        if (Icl < individualIndices[i + 1].CL)
                        {
                            // Found a relevant Level
                            i += 1;
                            break;
                        }

                        i++;
                    }
                    else
                    {
                        // reached to Level 5
                        // Found
                        break;
                    }
                }
            }

            wks.CL = weightCoefficients[i].CL;

            // CB
            i = 2; // index number, for Level 3
            var Icb = System.Math.Round(CalculatedIndices.Icb, 5, MidpointRounding.AwayFromZero);
            while (0 <= i)
            {
                if (Icb <= individualIndices[i].CB)
                {
                    if (i != 0)
                    {
                        if (individualIndices[i - 1].CB < Icb)
                        {
                            // Found a relevant Level
                            break;
                        }

                        i--;
                    }
                    else
                    {
                        // reached to Level 1
                        // Found
                        break;
                    }
                }
                else
                {
                    // For CB, comparison is only relevant till Level 3.
                    // FOR CB above Level 3, always choose Level 4, which is close to Level 3.
                    i += 1;
                    break;
                }
            }

            wks.CB = weightCoefficients[i].CB;

            return wks;
        }
        public static double CalculateOverallIndex(SimpleIndices I, WeightCoefficients W)
        {
            double overallIndex = 0.0;

            overallIndex = I.Iph * W.PH + I.Ido * W.DO + I.Ibod5 * W.BOD5 + I.Icod * W.COD + I.Inh4n * W.NH4N + I.Ino2n * W.NO2N + I.Ino3n * W.NO3N + I.Iss * W.SS + I.Icl * W.CL + I.Icb * W.CB;
            
            return overallIndex;
        }
        public static int GetLevel(double Temperature, double OverallIndex)
        {
            var db = CrystalClearDB.GetInstance();
            if (db == null) return 0;

            var weightIndices = db.GetWeightIndicesForTemperature(Temperature);

            var i = 0;
            while (i <= 4 && weightIndices[i].Overall < OverallIndex)
            {
                i++;
            }

            return i+1;
        }
    }
}
