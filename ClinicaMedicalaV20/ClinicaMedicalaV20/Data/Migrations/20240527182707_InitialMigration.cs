using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaMedicalaV20.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alerte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPacient = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Alarma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avertisment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrecventaCardiacaMax = table.Column<int>(type: "int", nullable: false),
                    UmiditateMax = table.Column<float>(type: "real", nullable: false),
                    TemperaturaMax = table.Column<int>(type: "int", nullable: false),
                    ECKMax = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Date_medicale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPacient = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    IstoricMedical = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alergii = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsultatiiCardiologice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Date_medicale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Date_senzori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPacient = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrecventaCardiaca = table.Column<float>(type: "real", nullable: false),
                    Umiditate = table.Column<float>(type: "real", nullable: false),
                    TemperaturaMax = table.Column<float>(type: "real", nullable: false),
                    ECG_pWave = table.Column<float>(type: "real", nullable: false),
                    ECG_qrsWave = table.Column<float>(type: "real", nullable: false),
                    ECG_tWave = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Date_senzori", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Medic = table.Column<int>(type: "int", nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacienti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDMedicApartinator = table.Column<int>(type: "int", nullable: false),
                    CNP = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Varsta = table.Column<int>(type: "int", nullable: false),
                    Judet = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Oras = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Strada = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Numar = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Bloc = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Apartament = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NumarTel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Profesie = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LocMunca = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacienti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recomandari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPacient = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    TipulRecomandarii = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Inceput = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Final = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteIndicatii = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomandari", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerte");

            migrationBuilder.DropTable(
                name: "Date_medicale");

            migrationBuilder.DropTable(
                name: "Date_senzori");

            migrationBuilder.DropTable(
                name: "Medici");

            migrationBuilder.DropTable(
                name: "Pacienti");

            migrationBuilder.DropTable(
                name: "Recomandari");
        }
    }
}
