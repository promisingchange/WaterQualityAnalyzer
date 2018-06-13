using System;
using System.Collections.Generic;
using System.Linq;

using CrystalClear.DL.Models;

namespace CrystalClear.DL
{
    public class CrystalClearDBUtil
    {
        public static List<EnvironmentalStandard> GenerateBaseDataForEnvironmentalStandard()
        {
            return new List<EnvironmentalStandard>
            {
                new EnvironmentalStandard { Id = 0, Level = 1, PH1 = 7.0, PH2 = 8.5, DO = 8, BOD5 = 1, COD = 1, NH4N = 0.05, NO2N = 0.005, NO3N = 1.0, SS = 10, CL = 30, CB = 500 },
                new EnvironmentalStandard { Id = 1, Level = 2, PH1 = 7.0, PH2 = 8.5, DO = 7.5, BOD5 = 2, COD = 1.5, NH4N = 0.1, NO2N = 0.01, NO3N = 5.0, SS = 20, CL = 30, CB = 1000 },
                new EnvironmentalStandard { Id = 2, Level = 3, PH1 = 6.5, PH2 = 8.5, DO = 5, BOD5 = 4, COD = 3, NH4N = 0.3, NO2N = 0.05, NO3N = 10.0, SS = 30, CL = 30, CB = 5000 },
                new EnvironmentalStandard { Id = 3, Level = 4, PH1 = 6.0, PH2 = 9.0, DO = 3, BOD5 = 7, COD = 6, NH4N = 0.7, NO2N = 0.1, NO3N = 13.0, SS = 50, CL = 40, CB = 0 },
                new EnvironmentalStandard { Id = 4, Level = 5, PH1 = 6.0, PH2 = 9.0, DO = 2, BOD5 = 10, COD = 10, NH4N = 1.2, NO2N = 0.3, NO3N = 17.0, SS = 70, CL = 50, CB = 0 }
            };
        }

        public static List<OxygenSolubility> GenerateBaseDataForOxygenSolubility()
        {
            return new List<OxygenSolubility>
            {
                new OxygenSolubility { Id = 0, Temperature = 0, DO = 14.62 },
                new OxygenSolubility { Id = 1, Temperature = 1, DO = 14.28 },
                new OxygenSolubility { Id = 2, Temperature = 2, DO = 13.84 },
                new OxygenSolubility { Id = 3, Temperature = 3, DO = 13.48 },
                new OxygenSolubility { Id = 4, Temperature = 4, DO = 13.13 },
                new OxygenSolubility { Id = 5, Temperature = 5, DO = 12.80 },
                new OxygenSolubility { Id = 6, Temperature = 6, DO = 12.48 },
                new OxygenSolubility { Id = 7, Temperature = 7, DO = 12.17 },
                new OxygenSolubility { Id = 8, Temperature = 8, DO = 11.87 },
                new OxygenSolubility { Id = 9, Temperature = 9, DO = 11.59 },
                new OxygenSolubility { Id = 10, Temperature = 10, DO = 11.33 },
                new OxygenSolubility { Id = 11, Temperature = 11, DO = 11.08 },
                new OxygenSolubility { Id = 12, Temperature = 12, DO = 10.83 },
                new OxygenSolubility { Id = 13, Temperature = 13, DO = 10.60 },
                new OxygenSolubility { Id = 14, Temperature = 14, DO = 10.37 },
                new OxygenSolubility { Id = 15, Temperature = 15, DO = 10.15 },
                new OxygenSolubility { Id = 16, Temperature = 16, DO = 9.95 },
                new OxygenSolubility { Id = 17, Temperature = 17, DO = 9.74 },
                new OxygenSolubility { Id = 18, Temperature = 18, DO = 9.54 },
                new OxygenSolubility { Id = 19, Temperature = 19, DO = 9.35 },
                new OxygenSolubility { Id = 20, Temperature = 20, DO = 9.17 },
                new OxygenSolubility { Id = 21, Temperature = 21, DO = 8.89 },
                new OxygenSolubility { Id = 22, Temperature = 22, DO = 8.83 },
                new OxygenSolubility { Id = 23, Temperature = 23, DO = 8.68 },
                new OxygenSolubility { Id = 24, Temperature = 24, DO = 8.53 },
                new OxygenSolubility { Id = 25, Temperature = 25, DO = 8.38 },
                new OxygenSolubility { Id = 26, Temperature = 26, DO = 8.22 },
                new OxygenSolubility { Id = 27, Temperature = 27, DO = 8.07 },
                new OxygenSolubility { Id = 28, Temperature = 28, DO = 7.92 },
                new OxygenSolubility { Id = 29, Temperature = 29, DO = 7.77 },
                new OxygenSolubility { Id = 30, Temperature = 30, DO = 7.63 }
            };
        }

        public static List<OxygenDO> GenerateBaseDataForOxygenDO()
        {
            return new List<OxygenDO>
            {
                new OxygenDO { Id = 0, Temperature = 10, DO = 14.00 },
                new OxygenDO { Id = 1, Temperature = 20, DO = 10.00 },
                new OxygenDO { Id = 2, Temperature = 30, DO = 8.292 }
            };
        }

        public static List<BaseIndex> GenerateBaseDataForBaseIndex()
        {
            return new List<BaseIndex>
            {
                new BaseIndex { Id = 0, Category = "II10", Level = 1, PH = 0.5, DO = 0.66667, BOD5 = 0.25, COD = 0.33333, NH4N = 0.16667, NO2N = 0.1, NO3N = 0.1, SS = 0.33333, CL = 1.00000, CB = 0.1 },
                new BaseIndex { Id = 1, Category = "II10", Level = 2, PH = 0.5, DO = 0.72222, BOD5 = 0.50, COD = 0.50000, NH4N = 0.33333, NO2N = 0.2, NO3N = 0.5, SS = 0.66667, CL = 1.00000, CB = 0.2 },
                new BaseIndex { Id = 2, Category = "II10", Level = 3, PH = 1.0, DO = 1.00000, BOD5 = 1.00, COD = 1.00000, NH4N = 1.00000, NO2N = 1.0, NO3N = 1.0, SS = 1.00000, CL = 1.00000, CB = 1.0 },
                new BaseIndex { Id = 3, Category = "II10", Level = 4, PH = 1.5, DO = 1.22222, BOD5 = 1.75, COD = 2.00000, NH4N = 2.33333, NO2N = 2.0, NO3N = 1.3, SS = 1.66667, CL = 1.33333, CB = 0.0 },
                new BaseIndex { Id = 4, Category = "II10", Level = 5, PH = 1.5, DO = 1.33333, BOD5 = 2.50, COD = 3.33333, NH4N = 4.00000, NO2N = 6.0, NO3N = 1.7, SS = 2.33333, CL = 1.66667, CB = 0.0 },

                new BaseIndex { Id = 5, Category = "II20", Level = 1, PH = 0.5, DO = 0.4, BOD5 = 0.25, COD = 0.33333, NH4N = 0.16667, NO2N = 0.1, NO3N = 0.1, SS = 0.33333, CL = 1.00000, CB = 0.1 },
                new BaseIndex { Id = 6, Category = "II20", Level = 2, PH = 0.5, DO = 0.5, BOD5 = 0.50, COD = 0.50000, NH4N = 0.33333, NO2N = 0.2, NO3N = 0.5, SS = 0.66667, CL = 1.00000, CB = 0.2 },
                new BaseIndex { Id = 7, Category = "II20", Level = 3, PH = 1.0, DO = 1.0, BOD5 = 1.00, COD = 1.00000, NH4N = 1.00000, NO2N = 1.0, NO3N = 1.0, SS = 1.00000, CL = 1.00000, CB = 1.0 },
                new BaseIndex { Id = 8, Category = "II20", Level = 4, PH = 1.5, DO = 1.4, BOD5 = 1.75, COD = 2.00000, NH4N = 2.33333, NO2N = 2.0, NO3N = 1.3, SS = 1.66667, CL = 1.33333, CB = 0.0 },
                new BaseIndex { Id = 9, Category = "II20", Level = 5, PH = 1.5, DO = 1.6, BOD5 = 2.50, COD = 3.33333, NH4N = 4.00000, NO2N = 6.0, NO3N = 1.7, SS = 2.33333, CL = 1.66667, CB = 0.0 },

                new BaseIndex { Id = 10, Category = "II30", Level = 1, PH = 0.5, DO = 0.08870, BOD5 = 0.25, COD = 0.33333, NH4N = 0.16667, NO2N = 0.1, NO3N = 0.1, SS = 0.33333, CL = 1.00000, CB = 0.1 },
                new BaseIndex { Id = 11, Category = "II30", Level = 2, PH = 0.5, DO = 0.24058, BOD5 = 0.50, COD = 0.50000, NH4N = 0.33333, NO2N = 0.2, NO3N = 0.5, SS = 0.66667, CL = 1.00000, CB = 0.2 },
                new BaseIndex { Id = 12, Category = "II30", Level = 3, PH = 1.0, DO = 1.00000, BOD5 = 1.00, COD = 1.00000, NH4N = 1.00000, NO2N = 1.0, NO3N = 1.0, SS = 1.00000, CL = 1.00000, CB = 1.0 },
                new BaseIndex { Id = 13, Category = "II30", Level = 4, PH = 1.5, DO = 1.60753, BOD5 = 1.75, COD = 2.00000, NH4N = 2.33333, NO2N = 2.0, NO3N = 1.3, SS = 1.66667, CL = 1.33333, CB = 0.0 },
                new BaseIndex { Id = 14, Category = "II30", Level = 5, PH = 1.5, DO = 1.91130, BOD5 = 2.50, COD = 3.33333, NH4N = 4.00000, NO2N = 6.0, NO3N = 1.7, SS = 2.33333, CL = 1.66667, CB = 0.0 },

                new BaseIndex { Id = 15, Category = "WK10", Level = 1, PH = 0.040, DO = 0.030, BOD5 = 0.079, COD = 0.059, NH4N = 0.119, NO2N = 0.198, NO3N = 0.198, SS = 0.059, CL = 0.020, CB = 0.198 },
                new BaseIndex { Id = 16, Category = "WK10", Level = 2, PH = 0.080, DO = 0.056, BOD5 = 0.080, COD = 0.080, NH4N = 0.121, NO2N = 0.201, NO3N = 0.080, SS = 0.060, CL = 0.040, CB = 0.201 },
                new BaseIndex { Id = 17, Category = "WK10", Level = 3, PH = 0.100, DO = 0.100, BOD5 = 0.100, COD = 0.100, NH4N = 0.100, NO2N = 0.100, NO3N = 0.100, SS = 0.100, CL = 0.100, CB = 0.100 },
                new BaseIndex { Id = 18, Category = "WK10", Level = 4, PH = 0.119, DO = 0.146, BOD5 = 0.102, COD = 0.089, NH4N = 0.076, NO2N = 0.089, NO3N = 0.137, SS = 0.107, CL = 0.134, CB = 0.000 },
                new BaseIndex { Id = 19, Category = "WK10", Level = 5, PH = 0.161, DO = 0.181, BOD5 = 0.096, COD = 0.072, NH4N = 0.060, NO2N = 0.040, NO3N = 0.142, SS = 0.103, CL = 0.145, CB = 0.000 },

                new BaseIndex { Id = 20, Category = "WK20", Level = 1, PH = 0.039, DO = 0.049, BOD5 = 0.078, COD = 0.058, NH4N = 0.117, NO2N = 0.194, NO3N = 0.194, SS = 0.058, CL = 0.019, CB = 0.194 },
                new BaseIndex { Id = 21, Category = "WK20", Level = 2, PH = 0.078, DO = 0.078, BOD5 = 0.078, COD = 0.078, NH4N = 0.118, NO2N = 0.196, NO3N = 0.078, SS = 0.059, CL = 0.039, CB = 0.196 },
                new BaseIndex { Id = 22, Category = "WK20", Level = 3, PH = 0.100, DO = 0.100, BOD5 = 0.100, COD = 0.100, NH4N = 0.100, NO2N = 0.100, NO3N = 0.100, SS = 0.100, CL = 0.100, CB = 0.100 },
                new BaseIndex { Id = 23, Category = "WK20", Level = 4, PH = 0.121, DO = 0.130, BOD5 = 0.104, COD = 0.091, NH4N = 0.078, NO2N = 0.091, NO3N = 0.109, SS = 0.109, CL = 0.136, CB = 0.000 },
                new BaseIndex { Id = 24, Category = "WK20", Level = 5, PH = 0.166, DO = 0.155, BOD5 = 0.099, COD = 0.075, NH4N = 0.062, NO2N = 0.041, NO3N = 0.146, SS = 0.106, CL = 0.149, CB = 0.000 },

                new BaseIndex { Id = 25, Category = "WK30", Level = 1, PH = 0.033, DO = 0.187, BOD5 = 0.066, COD = 0.050, NH4N = 0.100, NO2N = 0.166, NO3N = 0.166, SS = 0.050, CL = 0.017, CB = 0.166 },
                new BaseIndex { Id = 26, Category = "WK30", Level = 2, PH = 0.072, DO = 0.150, BOD5 = 0.072, COD = 0.072, NH4N = 0.108, NO2N = 0.181, NO3N = 0.072, SS = 0.054, CL = 0.036, CB = 0.181 },
                new BaseIndex { Id = 27, Category = "WK30", Level = 3, PH = 0.100, DO = 0.100, BOD5 = 0.100, COD = 0.100, NH4N = 0.100, NO2N = 0.100, NO3N = 0.100, SS = 0.100, CL = 0.100, CB = 0.100 },
                new BaseIndex { Id = 28, Category = "WK30", Level = 4, PH = 0.123, DO = 0.115, BOD5 = 0.106, COD = 0.092, NH4N = 0.079, NO2N = 0.092, NO3N = 0.142, SS = 0.111, CL = 0.139, CB = 0.000 },
                new BaseIndex { Id = 29, Category = "WK30", Level = 5, PH = 0.170, DO = 0.133, BOD5 = 0.102, COD = 0.076, NH4N = 0.064, NO2N = 0.042, NO3N = 0.150, SS = 0.109, CL = 0.153, CB = 0.000 },

                new BaseIndex { Id = 30, Category = "WI10", Level = 1, PH = 0.020, DO = 0.020, BOD5 = 0.020, COD = 0.020, NH4N = 0.020, NO2N = 0.020, NO3N = 0.020, SS = 0.020, CL = 0.020, CB = 0.020, Overall = 0.198 },
                new BaseIndex { Id = 31, Category = "WI10", Level = 2, PH = 0.040, DO = 0.040, BOD5 = 0.040, COD = 0.040, NH4N = 0.040, NO2N = 0.040, NO3N = 0.040, SS = 0.040, CL = 0.040, CB = 0.040, Overall = 0.402 },
                new BaseIndex { Id = 32, Category = "WI10", Level = 3, PH = 0.100, DO = 0.100, BOD5 = 0.100, COD = 0.100, NH4N = 0.100, NO2N = 0.100, NO3N = 0.100, SS = 0.100, CL = 0.100, CB = 0.100, Overall = 1.000 },
                new BaseIndex { Id = 33, Category = "WI10", Level = 4, PH = 0.178, DO = 0.178, BOD5 = 0.178, COD = 0.178, NH4N = 0.178, NO2N = 0.178, NO3N = 0.178, SS = 0.178, CL = 0.178, CB = 0.000, Overall = 1.606 },
                new BaseIndex { Id = 34, Category = "WI10", Level = 5, PH = 0.241, DO = 0.241, BOD5 = 0.241, COD = 0.241, NH4N = 0.241, NO2N = 0.241, NO3N = 0.241, SS = 0.241, CL = 0.241, CB = 0.000, Overall = 2.169 },

                new BaseIndex { Id = 35, Category = "WI20", Level = 1, PH = 0.019, DO = 0.019, BOD5 = 0.019, COD = 0.019, NH4N = 0.019, NO2N = 0.019, NO3N = 0.019, SS = 0.019, CL = 0.019, CB = 0.019, Overall = 0.194 },
                new BaseIndex { Id = 36, Category = "WI20", Level = 2, PH = 0.039, DO = 0.039, BOD5 = 0.039, COD = 0.039, NH4N = 0.039, NO2N = 0.039, NO3N = 0.039, SS = 0.039, CL = 0.039, CB = 0.039, Overall = 0.392 },
                new BaseIndex { Id = 37, Category = "WI20", Level = 3, PH = 0.100, DO = 0.100, BOD5 = 0.100, COD = 0.100, NH4N = 0.100, NO2N = 0.100, NO3N = 0.100, SS = 0.100, CL = 0.100, CB = 0.100, Overall = 1.000 },
                new BaseIndex { Id = 38, Category = "WI20", Level = 4, PH = 0.182, DO = 0.182, BOD5 = 0.182, COD = 0.182, NH4N = 0.182, NO2N = 0.182, NO3N = 0.182, SS = 0.182, CL = 0.182, CB = 0.000, Overall = 1.636 },
                new BaseIndex { Id = 39, Category = "WI20", Level = 5, PH = 0.248, DO = 0.248, BOD5 = 0.248, COD = 0.248, NH4N = 0.248, NO2N = 0.248, NO3N = 0.248, SS = 0.248, CL = 0.248, CB = 0.000, Overall = 2.236 },

                new BaseIndex { Id = 40, Category = "WI30", Level = 1, PH = 0.017, DO = 0.017, BOD5 = 0.017, COD = 0.017, NH4N = 0.017, NO2N = 0.017, NO3N = 0.017, SS = 0.017, CL = 0.017, CB = 0.017, Overall = 0.166 },
                new BaseIndex { Id = 41, Category = "WI30", Level = 2, PH = 0.036, DO = 0.036, BOD5 = 0.036, COD = 0.036, NH4N = 0.036, NO2N = 0.036, NO3N = 0.036, SS = 0.036, CL = 0.036, CB = 0.036, Overall = 0.362 },
                new BaseIndex { Id = 42, Category = "WI30", Level = 3, PH = 0.100, DO = 0.100, BOD5 = 0.100, COD = 0.100, NH4N = 0.100, NO2N = 0.100, NO3N = 0.100, SS = 0.100, CL = 0.100, CB = 0.100, Overall = 1.000 },
                new BaseIndex { Id = 43, Category = "WI30", Level = 4, PH = 0.185, DO = 0.185, BOD5 = 0.185, COD = 0.185, NH4N = 0.185, NO2N = 0.185, NO3N = 0.185, SS = 0.185, CL = 0.185, CB = 0.000, Overall = 1.664 },
                new BaseIndex { Id = 44, Category = "WI30", Level = 5, PH = 0.255, DO = 0.255, BOD5 = 0.255, COD = 0.255, NH4N = 0.255, NO2N = 0.255, NO3N = 0.255, SS = 0.255, CL = 0.255, CB = 0.000, Overall = 2.294 }
            };
        }
    }
}
