using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Migrations
{
    /// <inheritdoc />
    public partial class changeNamesOfTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Breeds_BreedId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Clients_ClientId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentMedicines_Appointments_AppointmentId",
                table: "AppointmentMedicines");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentMedicines_Medicines_MedicineId",
                table: "AppointmentMedicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Animals_AnimalId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentSurgeries_Appointments_AppointmentId",
                table: "AppointmentSurgeries");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentSurgeries_Surgeries_SurgeryId",
                table: "AppointmentSurgeries");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentVets_Appointments_AppointmentId",
                table: "AppointmentVets");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentVets_Vets_VetId",
                table: "AppointmentVets");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccinations_Animals_AnimalId",
                table: "Vaccinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccinations_TypeOfVaccines_TypeOfVaccineId",
                table: "Vaccinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Vets_Accounts_AccountId",
                table: "Vets");

            migrationBuilder.DropForeignKey(
                name: "FK_VetSpecialisations_Specialisations_SpecialisationId",
                table: "VetSpecialisations");

            migrationBuilder.DropForeignKey(
                name: "FK_VetSpecialisations_Vets_VetId",
                table: "VetSpecialisations");

            migrationBuilder.DropForeignKey(
                name: "FK_Weights_Animals_AnimalId",
                table: "Weights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weights",
                table: "Weights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VetSpecialisations",
                table: "VetSpecialisations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vets",
                table: "Vets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccinations",
                table: "Vaccinations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeOfVaccines",
                table: "TypeOfVaccines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surgeries",
                table: "Surgeries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialisations",
                table: "Specialisations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicines",
                table: "Medicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Breeds",
                table: "Breeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentVets",
                table: "AppointmentVets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentSurgeries",
                table: "AppointmentSurgeries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentMedicines",
                table: "AppointmentMedicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animals",
                table: "Animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Weights",
                newName: "Weight");

            migrationBuilder.RenameTable(
                name: "VetSpecialisations",
                newName: "VetSpecialisation");

            migrationBuilder.RenameTable(
                name: "Vets",
                newName: "Vet");

            migrationBuilder.RenameTable(
                name: "Vaccinations",
                newName: "Vaccination");

            migrationBuilder.RenameTable(
                name: "TypeOfVaccines",
                newName: "TypeOfVaccine");

            migrationBuilder.RenameTable(
                name: "Surgeries",
                newName: "Surgery");

            migrationBuilder.RenameTable(
                name: "Specialisations",
                newName: "Specialisation");

            migrationBuilder.RenameTable(
                name: "Medicines",
                newName: "Medicine");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameTable(
                name: "Breeds",
                newName: "Breed");

            migrationBuilder.RenameTable(
                name: "AppointmentVets",
                newName: "AppointmentVet");

            migrationBuilder.RenameTable(
                name: "AppointmentSurgeries",
                newName: "AppointmentSurgery");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameTable(
                name: "AppointmentMedicines",
                newName: "AppointmentMedicine");

            migrationBuilder.RenameTable(
                name: "Animals",
                newName: "Animal");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_VetSpecialisations_VetId",
                table: "VetSpecialisation",
                newName: "IX_VetSpecialisation_VetId");

            migrationBuilder.RenameIndex(
                name: "IX_Vets_AccountId",
                table: "Vet",
                newName: "IX_Vet_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccinations_TypeOfVaccineId",
                table: "Vaccination",
                newName: "IX_Vaccination_TypeOfVaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccinations_AnimalId",
                table: "Vaccination",
                newName: "IX_Vaccination_AnimalId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentVets_VetId",
                table: "AppointmentVet",
                newName: "IX_AppointmentVet_VetId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentSurgeries_SurgeryId",
                table: "AppointmentSurgery",
                newName: "IX_AppointmentSurgery_SurgeryId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_AnimalId",
                table: "Appointment",
                newName: "IX_Appointment_AnimalId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentMedicines_MedicineId",
                table: "AppointmentMedicine",
                newName: "IX_AppointmentMedicine_MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_ClientId",
                table: "Animal",
                newName: "IX_Animal_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_BreedId",
                table: "Animal",
                newName: "IX_Animal_BreedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weight",
                table: "Weight",
                columns: new[] { "AnimalId", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_VetSpecialisation",
                table: "VetSpecialisation",
                columns: new[] { "SpecialisationId", "VetId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vet",
                table: "Vet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccination",
                table: "Vaccination",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeOfVaccine",
                table: "TypeOfVaccine",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surgery",
                table: "Surgery",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialisation",
                table: "Specialisation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicine",
                table: "Medicine",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Breed",
                table: "Breed",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentVet",
                table: "AppointmentVet",
                columns: new[] { "AppointmentId", "VetId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentSurgery",
                table: "AppointmentSurgery",
                columns: new[] { "AppointmentId", "SurgeryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentMedicine",
                table: "AppointmentMedicine",
                columns: new[] { "AppointmentId", "MedicineId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animal",
                table: "Animal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Breed_BreedId",
                table: "Animal",
                column: "BreedId",
                principalTable: "Breed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Client_ClientId",
                table: "Animal",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Animal_AnimalId",
                table: "Appointment",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentMedicine_Appointment_AppointmentId",
                table: "AppointmentMedicine",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentMedicine_Medicine_MedicineId",
                table: "AppointmentMedicine",
                column: "MedicineId",
                principalTable: "Medicine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentSurgery_Appointment_AppointmentId",
                table: "AppointmentSurgery",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentSurgery_Surgery_SurgeryId",
                table: "AppointmentSurgery",
                column: "SurgeryId",
                principalTable: "Surgery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentVet_Appointment_AppointmentId",
                table: "AppointmentVet",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentVet_Vet_VetId",
                table: "AppointmentVet",
                column: "VetId",
                principalTable: "Vet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccination_Animal_AnimalId",
                table: "Vaccination",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccination_TypeOfVaccine_TypeOfVaccineId",
                table: "Vaccination",
                column: "TypeOfVaccineId",
                principalTable: "TypeOfVaccine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vet_Account_AccountId",
                table: "Vet",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VetSpecialisation_Specialisation_SpecialisationId",
                table: "VetSpecialisation",
                column: "SpecialisationId",
                principalTable: "Specialisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VetSpecialisation_Vet_VetId",
                table: "VetSpecialisation",
                column: "VetId",
                principalTable: "Vet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weight_Animal_AnimalId",
                table: "Weight",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Breed_BreedId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Client_ClientId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Animal_AnimalId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentMedicine_Appointment_AppointmentId",
                table: "AppointmentMedicine");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentMedicine_Medicine_MedicineId",
                table: "AppointmentMedicine");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentSurgery_Appointment_AppointmentId",
                table: "AppointmentSurgery");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentSurgery_Surgery_SurgeryId",
                table: "AppointmentSurgery");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentVet_Appointment_AppointmentId",
                table: "AppointmentVet");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentVet_Vet_VetId",
                table: "AppointmentVet");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccination_Animal_AnimalId",
                table: "Vaccination");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccination_TypeOfVaccine_TypeOfVaccineId",
                table: "Vaccination");

            migrationBuilder.DropForeignKey(
                name: "FK_Vet_Account_AccountId",
                table: "Vet");

            migrationBuilder.DropForeignKey(
                name: "FK_VetSpecialisation_Specialisation_SpecialisationId",
                table: "VetSpecialisation");

            migrationBuilder.DropForeignKey(
                name: "FK_VetSpecialisation_Vet_VetId",
                table: "VetSpecialisation");

            migrationBuilder.DropForeignKey(
                name: "FK_Weight_Animal_AnimalId",
                table: "Weight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weight",
                table: "Weight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VetSpecialisation",
                table: "VetSpecialisation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vet",
                table: "Vet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccination",
                table: "Vaccination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeOfVaccine",
                table: "TypeOfVaccine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surgery",
                table: "Surgery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialisation",
                table: "Specialisation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicine",
                table: "Medicine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Breed",
                table: "Breed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentVet",
                table: "AppointmentVet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentSurgery",
                table: "AppointmentSurgery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentMedicine",
                table: "AppointmentMedicine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animal",
                table: "Animal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Weight",
                newName: "Weights");

            migrationBuilder.RenameTable(
                name: "VetSpecialisation",
                newName: "VetSpecialisations");

            migrationBuilder.RenameTable(
                name: "Vet",
                newName: "Vets");

            migrationBuilder.RenameTable(
                name: "Vaccination",
                newName: "Vaccinations");

            migrationBuilder.RenameTable(
                name: "TypeOfVaccine",
                newName: "TypeOfVaccines");

            migrationBuilder.RenameTable(
                name: "Surgery",
                newName: "Surgeries");

            migrationBuilder.RenameTable(
                name: "Specialisation",
                newName: "Specialisations");

            migrationBuilder.RenameTable(
                name: "Medicine",
                newName: "Medicines");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameTable(
                name: "Breed",
                newName: "Breeds");

            migrationBuilder.RenameTable(
                name: "AppointmentVet",
                newName: "AppointmentVets");

            migrationBuilder.RenameTable(
                name: "AppointmentSurgery",
                newName: "AppointmentSurgeries");

            migrationBuilder.RenameTable(
                name: "AppointmentMedicine",
                newName: "AppointmentMedicines");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameTable(
                name: "Animal",
                newName: "Animals");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_VetSpecialisation_VetId",
                table: "VetSpecialisations",
                newName: "IX_VetSpecialisations_VetId");

            migrationBuilder.RenameIndex(
                name: "IX_Vet_AccountId",
                table: "Vets",
                newName: "IX_Vets_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccination_TypeOfVaccineId",
                table: "Vaccinations",
                newName: "IX_Vaccinations_TypeOfVaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccination_AnimalId",
                table: "Vaccinations",
                newName: "IX_Vaccinations_AnimalId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentVet_VetId",
                table: "AppointmentVets",
                newName: "IX_AppointmentVets_VetId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentSurgery_SurgeryId",
                table: "AppointmentSurgeries",
                newName: "IX_AppointmentSurgeries_SurgeryId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentMedicine_MedicineId",
                table: "AppointmentMedicines",
                newName: "IX_AppointmentMedicines_MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_AnimalId",
                table: "Appointments",
                newName: "IX_Appointments_AnimalId");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_ClientId",
                table: "Animals",
                newName: "IX_Animals_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_BreedId",
                table: "Animals",
                newName: "IX_Animals_BreedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weights",
                table: "Weights",
                columns: new[] { "AnimalId", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_VetSpecialisations",
                table: "VetSpecialisations",
                columns: new[] { "SpecialisationId", "VetId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vets",
                table: "Vets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccinations",
                table: "Vaccinations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeOfVaccines",
                table: "TypeOfVaccines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surgeries",
                table: "Surgeries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialisations",
                table: "Specialisations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicines",
                table: "Medicines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Breeds",
                table: "Breeds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentVets",
                table: "AppointmentVets",
                columns: new[] { "AppointmentId", "VetId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentSurgeries",
                table: "AppointmentSurgeries",
                columns: new[] { "AppointmentId", "SurgeryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentMedicines",
                table: "AppointmentMedicines",
                columns: new[] { "AppointmentId", "MedicineId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animals",
                table: "Animals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Breeds_BreedId",
                table: "Animals",
                column: "BreedId",
                principalTable: "Breeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Clients_ClientId",
                table: "Animals",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentMedicines_Appointments_AppointmentId",
                table: "AppointmentMedicines",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentMedicines_Medicines_MedicineId",
                table: "AppointmentMedicines",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Animals_AnimalId",
                table: "Appointments",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentSurgeries_Appointments_AppointmentId",
                table: "AppointmentSurgeries",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentSurgeries_Surgeries_SurgeryId",
                table: "AppointmentSurgeries",
                column: "SurgeryId",
                principalTable: "Surgeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentVets_Appointments_AppointmentId",
                table: "AppointmentVets",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentVets_Vets_VetId",
                table: "AppointmentVets",
                column: "VetId",
                principalTable: "Vets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccinations_Animals_AnimalId",
                table: "Vaccinations",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccinations_TypeOfVaccines_TypeOfVaccineId",
                table: "Vaccinations",
                column: "TypeOfVaccineId",
                principalTable: "TypeOfVaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vets_Accounts_AccountId",
                table: "Vets",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VetSpecialisations_Specialisations_SpecialisationId",
                table: "VetSpecialisations",
                column: "SpecialisationId",
                principalTable: "Specialisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VetSpecialisations_Vets_VetId",
                table: "VetSpecialisations",
                column: "VetId",
                principalTable: "Vets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weights_Animals_AnimalId",
                table: "Weights",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
