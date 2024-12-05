


using System;
using System.Collections.Generic;

namespace HospitalManagementSystem
{
    public abstract class Person
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string MobileNo { get; set; }
        protected Person(string name, int? age=null, string mobileNo=null)
        {
            Name = name;
            Age = age;
            MobileNo = mobileNo;
        }
        public abstract void DisplayDetails();
    }

    // Patient class(EMON)
    public class Patient : Person
    {
        public string PatientId { get; set; }
        public string Illness { get; set; }
        public Patient(string name, int age, string mobileNo, string patientId, string illness) : base(name, age, mobileNo)
        {
            PatientId = patientId;
            Illness = illness;
        }
        public override void DisplayDetails()
        {
            Console.WriteLine($"Patient ID: {PatientId}, Name: {Name}, Age: {Age}, Illness: {Illness}");
        }
    }

    // Doctor class(NURJAHAN)
    public class Doctor : Person
    {
        public string DoctorId { get; set; }
        public string Specialization { get; set; }
        public Doctor(string name, string doctorId, string specialization, int? age = null, string mobileNo = null) : base(name, age, mobileNo)
        {
            DoctorId = doctorId;
            Specialization = specialization;
        }
        public override void DisplayDetails()
        {
            Console.WriteLine($"Doctor ID: {DoctorId}, Name: {Name}, Age: {(Age.HasValue ? Age.ToString() : "N/A")}, Mobile: {MobileNo ?? "N/A"}, Specialization: {Specialization}");
        }
    }

    // Staff class(JANNAT)
    public class Staff : Person
    {
        public string StaffId { get; set; }
        public string Role { get; set; }
        public Staff(string name,string staffId, string role, int? age = null, string mobileNo = null) : base(name, age,mobileNo)
        {
            StaffId = staffId;
            Role = role;
        }
        public override void DisplayDetails()
        {
            Console.WriteLine($"Staff Name: {Name}, Age: {(Age.HasValue ? Age.ToString() : "N/A")}, Mobile: {MobileNo ?? "N/A"}, Role: {Role}");
        }
    }

    // Appointment class(KIBREYA)
    public class Appointment
    {
        public string AppointmentId { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Appointment(string appointmentId, Patient patient, Doctor doctor, DateTime date)
        {
            AppointmentId = appointmentId;
            Patient = patient;
            Doctor = doctor;
            AppointmentDate = date;
        }
        public void DisplayDetails()
        {
            Console.WriteLine($"Appointment ID: {AppointmentId}, Patient: {Patient.Name}, Doctor: {Doctor.Name}, Date: {AppointmentDate}");
        }
    }

    //abu bakar
    public class Hospital
    {
        private static int patientCount = 0;
        private List<Patient> patients = new List<Patient>();
        private List<Doctor> doctors = new List<Doctor>();
        private List<Staff> staffs = new List<Staff>();
        private List<Appointment> appointments = new List<Appointment>();

        //add Patient 
        public void AddPatient(string name, int age,string mobileNo, string illness)
        {
            patientCount++;
            string patientId = $"P{patientCount:D4}";
            var patient = new Patient(name, age,mobileNo, patientId, illness);
            patients.Add(patient);
            Console.WriteLine("\nPatient added successfully!");
        }

        //remove patient
        public void RemovePatient(string patientId)
        {
            var patient = patients.Find(p => p.PatientId == patientId);

            if (patient != null)
            {
                patients.Remove(patient);
                Console.WriteLine("\nPatient removed successfully!");
            }
        }

        //add doctor
        public void AddDoctor(string name, string specialization, int? age = null, string mobileNo = null)
        {
            string doctorId = $"D{doctors.Count + 1:D4}";
            var doctor = new Doctor(name,  doctorId, specialization, age, mobileNo);
            doctors.Add(doctor);
            Console.WriteLine("\nDoctor added successfully!");
        }

        //remove doctor
        public void RemoveDoctor(string doctorId) {
        var doctor = doctors.Find(d => d.DoctorId == doctorId);

            if(doctor != null)
            {
                doctors.Remove(doctor);
                Console.WriteLine("\nDoctor removed successfully");
            }
        }

        //add staff
        public void AddStaff(string name, string role, int? age = null, string mobileNo = null)
        {
            string staffId = $"S{staffs.Count + 1:D4}";
            var staff = new Staff(name, staffId, role, age, mobileNo);
            staffs.Add(staff);
            Console.WriteLine("\nStaff added successfully!");
        }

        public void ScheduleAppointment(string patientId, string doctorId, DateTime date)
        {
            var patient = patients.Find(p => p.PatientId == patientId);
            var doctor = doctors.Find(d => d.DoctorId == doctorId);
            if (patient != null && doctor != null)
            {
                var appointment = new Appointment($"A{appointments.Count + 1:D4}", patient, doctor, date);
                appointments.Add(appointment);
                Console.WriteLine("\nAppointment scheduled successfully!");
            }
            else
            {
                Console.WriteLine("\nPatient or Doctor not found!");
            }
        }

        public void DisplayPatients()
        {
            Console.WriteLine("\nPatients List:");
            if (patients.Count == 0)
            {
                Console.WriteLine("\nNo patients found.");
            }
            else
            {
                foreach (var patient in patients)
                {
                    patient.DisplayDetails();
                }

                //  option to remove a patient
                Console.WriteLine("\nEnter patient ID to remove or press Enter to skip:"); 
                string patientId = Console.ReadLine();
                if (!string.IsNullOrEmpty(patientId)) {
                    RemovePatient(patientId); 
                }

            }
        }

        public void DisplayDoctors()
        {
            Console.WriteLine("\nDoctors List:");
            if (doctors.Count == 0)
            {
                Console.WriteLine("\nNo doctors found");
            }
            else {
                foreach (var doctor in doctors)
                {
                    doctor.DisplayDetails();
                }

                //  option to remove a doctor
                Console.WriteLine("\nEnter patient ID to remove or press Enter to skip:");
                string doctorId = Console.ReadLine();
                if (!string.IsNullOrEmpty(doctorId))
                {
                    RemoveDoctor(doctorId);
                }
            }
         
        }

        public void DisplayStaff()
        {
            Console.WriteLine("\nStaff List: ");
            if (staffs.Count == 0)
            {
                Console.WriteLine("\nNo staffs found");
            }
            else { 
            foreach(var staff in staffs)
                {
                    staff.DisplayDetails();
                }
                //  option to remove a doctor
                Console.WriteLine("\nEnter patient ID to remove or press Enter to skip:");
                string staffId = Console.ReadLine();
                if (!string.IsNullOrEmpty(staffId))
                {
                    RemoveDoctor(staffId);
                }
            }
        }

        public void DisplayAppointments()
        {
            Console.WriteLine("\nAppointments List:");
            if(appointments.Count == 0)
            {
                Console.WriteLine("\nNo appointment schedule found");
            }
            else
            {
                foreach (var appointment in appointments)
                {
                    appointment.DisplayDetails();
                }
            }
          
        }
    }

    class Program
    {
        static void Main()
        {
            var hospital = new Hospital();
         

            Console.WriteLine("\tWelcome To Hospital Management System");

            while (true)
            {
                Console.WriteLine("\nFeatures: ");

                Console.WriteLine("\n1.Patient");
                Console.WriteLine("2.Doctor");
                Console.WriteLine("3.Staff");
                Console.WriteLine("4.Appointment");
                Console.WriteLine("5.Services");
                Console.WriteLine("6.Daily Report");
                Console.WriteLine("7.Exit");

                Console.Write("\n Select Option: ");

                string input = Console.ReadLine();

                //Patient option
                if (input == "1")
                {
                    string selectOption;
                    Console.WriteLine("1.Add Patient");
                    Console.WriteLine("2.Old Patients");

                    Console.Write("\nSelect Option: ");
                    selectOption =  Console.ReadLine();

                    if (selectOption == "1")
                    {
                        //Patient registration with name,age,mobile
                        Console.Write("Enter patient name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter patient age: ");
                        int age = int.Parse(Console.ReadLine());
                        Console.Write("Enter mobile number: ");
                        string mobileNo = Console.ReadLine();
                        Console.Write("Enter patient illness: ");
                        string illness = Console.ReadLine();
                        hospital.AddPatient(name, age, mobileNo, illness);

                        //hospital.DisplayPatients();
                    }
                    else if (selectOption == "2") { 
                       Console.WriteLine("patient list");
                        hospital.DisplayPatients();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
                //Doctor option
                else if(input == "2")
                {
                    string selectOption;
                    Console.WriteLine("1.Add Doctor");
                    Console.WriteLine("2.Doctors List");

                    Console.Write("\nSelect Option: ");
                    selectOption = Console.ReadLine();

                    if (selectOption == "1")
                    {
                        //doctor registration with name,specialist
                        Console.Write("Enter doctor name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter doctor specialization: ");
                        string specialist = Console.ReadLine();
                        Console.Write("Enter doctor age (optional, press Enter to skip): "); 
                        string ageInput = Console.ReadLine(); 
                        int? age = string.IsNullOrEmpty(ageInput) ? (int?)null : int.Parse(ageInput);
                        Console.Write("Enter mobile number (optional, press Enter to skip): "); 
                        string mobileNo = Console.ReadLine();
                        hospital.AddDoctor(name,specialist, age, mobileNo);

                        //hospital.DisplayPatients();
                    }
                    else if (selectOption == "2")
                    {
                      
                        hospital.DisplayDoctors();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
                //Staff option
                else if(input == "3")
                {
                    string selectOption;
                    Console.WriteLine("1.Add Staff");
                    Console.WriteLine("2.Staff List");

                    Console.Write("\nSelect Option: ");
                    selectOption = Console.ReadLine();

                    if (selectOption == "1")
                    {
                        //Patient registration with name,role
                        Console.Write("Enter staff name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter role: ");
                        string role = Console.ReadLine();
                        string ageInput = Console.ReadLine();
                        int? age = string.IsNullOrEmpty(ageInput) ? (int?)null : int.Parse(ageInput);
                        Console.Write("Enter mobile number (optional, press Enter to skip): ");
                        string mobileNo = Console.ReadLine();

                        hospital.AddStaff(name, role, age, mobileNo);

                        //hospital.DisplayPatients();
                    }
                    else if (selectOption == "2")
                    {
                        
                        hospital.DisplayStaff();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
                //Appointment option
                else if(input == "4")
                {
                    string selectOption;
                    Console.WriteLine("1.Schedule appointment");
                    Console.WriteLine("2.Appointment List");

                    Console.Write("\nSelect Option: ");
                    selectOption = Console.ReadLine();

                    if (selectOption == "1") {

                        //appointment schedule with patientId,doctorId,date

                        Console.Write("Enter patientId: ");
                        string patientId = Console.ReadLine();
                        Console.Write("Enter doctorId: ");
                        string doctorId = Console.ReadLine();
                        Console.Write("Enter appointment date (YYYY-MM-DD): ");
                        DateTime date;

                        if (DateTime.TryParse(Console.ReadLine(), out date))
                        {
                            hospital.ScheduleAppointment(patientId, doctorId, date);
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                        }
                    }
                    else if (selectOption == "2"){
                        hospital.DisplayAppointments();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }

                   
                }
                else {
                    Console.WriteLine("Invalid option. Please try again.");
                }

                
            }

           
        }
    }
}
