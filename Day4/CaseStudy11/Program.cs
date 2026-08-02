using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HospitalSystem system = new HospitalSystem();
            system.Patients.Add(new Patient { Id = "P1001", Name = "Arun", Age = 30 });
            system.Patients.Add(new Patient { Id = "P1002", Name = "Meena", Age = 26 });
            system.Doctors.Add(new Doctor { Id = "D101", Name = "Dr. Kiran", Specialty = "Cardiology" });
            system.Appointments.Add(new Appointment { Id = 1, PatientId = "P1001", DoctorId = "D101", Date = DateTime.Today });
            system.Bills.Add(new Bill { Id = 1, PatientId = "P1001", Amount = 5000m });

            Console.WriteLine(system["P1001"].Print());
            object dashboard = new
            {
                TotalPatients = system.Patients.GetAll().Count,
                TotalDoctors = system.Doctors.GetAll().Count,
                Revenue = system.Bills.GetAll().Sum(delegate(Bill bill) { return bill.Amount; })
            };
            Console.WriteLine(dashboard);
        }
    }

    public interface IPrintable
    {
        string Print();
    }

    public abstract class Person : IPrintable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public abstract string Print();
    }

    public class Patient : Person
    {
        public int Age { get; set; }
        public override string Print() { return Id + " - " + Name + " - Age " + Age; }
    }

    public class Doctor : Person
    {
        public string Specialty { get; set; }
        public override string Print() { return Id + " - " + Name + " - " + Specialty; }
    }

    public class Appointment
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime Date { get; set; }
    }

    public class Bill
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public decimal Amount { get; set; }
    }

    public class GenericRepository<T>
    {
        private readonly List<T> _items = new List<T>();
        public void Add(T item) { _items.Add(item); }
        public List<T> GetAll() { return _items; }
    }

    public class PatientRepository : GenericRepository<Patient>
    {
        public Patient this[string id]
        {
            get { return GetAll().First(delegate(Patient patient) { return patient.Id == id; }); }
        }
    }

    public class HospitalSystem
    {
        public HospitalSystem()
        {
            Patients = new PatientRepository();
            Doctors = new GenericRepository<Doctor>();
            Appointments = new GenericRepository<Appointment>();
            Bills = new GenericRepository<Bill>();
        }

        public PatientRepository Patients { get; private set; }
        public GenericRepository<Doctor> Doctors { get; private set; }
        public GenericRepository<Appointment> Appointments { get; private set; }
        public GenericRepository<Bill> Bills { get; private set; }
        public Patient this[string id] { get { return Patients[id]; } }
    }
}