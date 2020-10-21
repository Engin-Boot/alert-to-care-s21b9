using System;
using AlertToCareApi.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlertToCareApi.EntriesValidator
{
    public abstract class PatientInfoValidator
    {
        private static bool ValidatePatientInfo(Patients patient)
        {
            if (CheckIfLengthIsValid(patient.ContactNo, 10) && CheckIfNameIsValid(patient.PatientName))
            {
                return true;
            }
            return false;

        }
        private static bool CheckForBedsAvailability(int countOfAvailableBeds)
        {
            if (countOfAvailableBeds == 0)
                return false;
            else
                return true;
        }

        // ReSharper disable once RedundantAssignment
        public static void ValidateInfoAndCheckForAvailability(Patients patient, int countOfAvailableBeds, ref string message)
        {
            bool validInfo = false;
            ValidateInfoAndCheckForAvailability(patient, countOfAvailableBeds, ref validInfo, ref message);
        }

        // ReSharper disable once RedundantAssignment
        public static void ValidateInfoAndCheckForAvailability(Patients patient, int countOfAvailableBeds, ref bool validInfo)
        {
            string message = null;
            ValidateInfoAndCheckForAvailability(patient, countOfAvailableBeds, ref validInfo, ref message);
        }

        // ReSharper disable once RedundantAssignment
        public static void ValidateInfoAndCheckForAvailability(Patients patient, int countOfAvailableBeds,
            ref bool validInfo, ref string message)
        {
           
            if (ValidatePatientInfo(patient))
            {
                if (CheckForBedsAvailability(countOfAvailableBeds))
                {
                    validInfo = true;
                    message = "";
                }
                else
                {
                    validInfo = false;
                    message = "All Beds Are Occupied, Cannot Add More Patients";
                }
            }
            else
            {
                validInfo = false;
                message = "Patient Information Is Incorrect, Please Recheck";
            }
        }
        private static bool CheckIfLengthIsValid(string contactNo, int length)
        {
            if (contactNo.Length == length)
            {
                return true;
            }
            return false;
        }

        private static bool CheckIfNameIsValid(string patientName)
        {
            if (patientName == "" || !Regex.Match(patientName, @"^[A-Za-z]+[\s][A-Za-z]+$").Success)
            {
                return false;
            }
            return true;

        }

        public static void DeleteVitalLogsForDischargedPatient(int patientId)
        {
            ConfigDbContext context = new ConfigDbContext();
            var vitalsStore = context.VitalsLogs.ToList();
            var vitalsToRemove = vitalsStore.Where(item => item.PatientId == patientId).ToList();
            foreach (VitalsLogs vitals in vitalsToRemove)
            {
                context.Remove(vitals);
                context.SaveChanges();
            }
        }
    }
}
