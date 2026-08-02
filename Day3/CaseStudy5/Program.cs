using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Ward wardA = new Ward("W1", "Cardiac Ward");
            Ward wardB = new Ward("W2", "General Surgery");
            Patient patient1 = new Patient("PT1", "Mohan", wardA);
            Patient patient2 = new Patient("PT2", "Divya", wardA);
            Patient patient3 = new Patient("PT3", "Rakesh", wardB);
            Surgeon surgeon1 = new Surgeon("S1", "Dr. Meera", SurgeonType.Senior);
            Surgeon surgeon2 = new Surgeon("S2", "Dr. Arun", SurgeonType.NonSenior);

            List<Hospital> hospitals = new List<Hospital>
            {
                new Hospital("City Hospital", new List<Surgeon> { surgeon1, surgeon2 }),
                new Hospital("Metro Care", new List<Surgeon> { surgeon1 })
            };

            List<Operation> operations = new List<Operation>
            {
                new Operation("OP1", surgeon1, patient1, hospitals[0], new DateOnly(2026, 8, 2)),
                new Operation("OP2", surgeon1, patient2, hospitals[1], new DateOnly(2026, 8, 3)),
                new Operation("OP3", surgeon2, patient3, hospitals[0], new DateOnly(2026, 8, 4))
            };

            SurgeonManagementSystem system = new SurgeonManagementSystem(operations, new List<Patient> { patient1, patient2, patient3 });

            Console.WriteLine("Case Study 5");
            Console.WriteLine("Total number of patients being operated: " + system.GetTotalPatientsOperated());
            PrintPatients("Patients operated by Dr. Meera", system.GetPatientsBySurgeon("Dr. Meera"));
            PrintPatients("Patients admitted to Cardiac Ward", system.GetPatientsByWard("Cardiac Ward"));
        }

        private static void PrintPatients(string title, IEnumerable<Patient> patients)
        {
            Console.WriteLine(title);
            foreach (Patient patient in patients.GroupBy(delegate(Patient p) { return p.Id; }).Select(delegate(IGrouping<string, Patient> group) { return group.First(); }))
            {
                Console.WriteLine("- " + patient.Name);
            }
        }
    }

    public class Ward
    {
        public Ward(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
    }

    public class Patient
    {
        public Patient(string id, string name, Ward ward)
        {
            Id = id;
            Name = name;
            Ward = ward;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public Ward Ward { get; private set; }
    }

    public class Hospital
    {
        public Hospital(string name, List<Surgeon> surgeons)
        {
            Name = name;
            Surgeons = surgeons;
        }

        public string Name { get; private set; }
        public List<Surgeon> Surgeons { get; private set; }
    }

    public enum SurgeonType
    {
        Senior,
        NonSenior
    }

    public class Surgeon
    {
        public Surgeon(string id, string name, SurgeonType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public SurgeonType Type { get; private set; }
    }

    public class Operation
    {
        public Operation(string id, Surgeon surgeon, Patient patient, Hospital hospital, DateOnly date)
        {
            Id = id;
            Surgeon = surgeon;
            Patient = patient;
            Hospital = hospital;
            Date = date;
        }

        public string Id { get; private set; }
        public Surgeon Surgeon { get; private set; }
        public Patient Patient { get; private set; }
        public Hospital Hospital { get; private set; }
        public DateOnly Date { get; private set; }
    }

    public class SurgeonManagementSystem
    {
        private readonly List<Operation> _operations;
        private readonly List<Patient> _patients;

        public SurgeonManagementSystem(List<Operation> operations, List<Patient> patients)
        {
            _operations = operations;
            _patients = patients;
        }

        public int GetTotalPatientsOperated()
        {
            return _operations.Select(delegate(Operation operation) { return operation.Patient.Id; }).Distinct().Count();
        }

        public List<Patient> GetPatientsBySurgeon(string surgeonName)
        {
            return _operations.Where(delegate(Operation operation) { return operation.Surgeon.Name.Equals(surgeonName, StringComparison.OrdinalIgnoreCase); })
                .Select(delegate(Operation operation) { return operation.Patient; })
                .ToList();
        }

        public List<Patient> GetPatientsByWard(string wardName)
        {
            return _patients.Where(delegate(Patient patient) { return patient.Ward.Name.Equals(wardName, StringComparison.OrdinalIgnoreCase); }).ToList();
        }
    }
}