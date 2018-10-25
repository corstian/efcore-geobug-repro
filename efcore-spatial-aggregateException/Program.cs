using Boerman.Core.Communication;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NotVisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace efcore_spatial_aggregateException
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var context = new BuggyDbContext())
            {
                context.Database.Migrate();
            }

            Console.WriteLine("Hello World!");

            await ImportAirports();
        }

        public static async Task ImportAirports()
        {
            var _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            #region Data download / preparation
            var stream = await new HttpCommand()
                .SetAddress("http://ourairports.com/data/airports.csv")
                .SetMethod("GET")
                .GetResponse<Stream>();

            var parser = new CsvTextFieldParser(stream)
            {
                CompatibilityMode = true,   // I'm not sure what this does, but it worked in .NET Framework, so we should be allright.
                Delimiters = new[] { "," }
            };

            var airportCollection = new List<Airfield>();

            // We're skipping the first record because this one contains all the column definitions
            bool firstRecordHasBeenSkipped = false;

            while (!parser.EndOfData)
            {
                var record = parser.ReadFields();

                if (!firstRecordHasBeenSkipped)
                {
                    firstRecordHasBeenSkipped = true;
                    continue;
                }

                var ourAirfieldsId = Convert.ToInt32(record?[0]);

                airportCollection.Add(new Airfield
                {
                    Id = Guid.NewGuid(),
                    OurAirfieldsId = ourAirfieldsId,
                    Icao = record?[1],
                    Type = record?[2],
                    Name = record?[3],
                    Location = _geometryFactory.CreatePoint(new Coordinate(
                        record?[4].ToDouble() ?? 0,
                        record?[5].ToDouble() ?? 0,
                        record?[6].ToInt() ?? 0)),
                    Continent = record?[7],
                    Country = record?[8],
                    Region = record?[9],
                    Muncipality = record?[10],
                    HasScheduledService = record?[11] != "no",
                    Iata = record?[13],
                    HomePage = record?[15]
                });
            }
            #endregion

            using (var context = new BuggyDbContext())
            {
                var airfieldsInDb = context.Airfields.Select(q => new { q.OurAirfieldsId, q.Id }).Distinct().ToArray();

                // Insert the new airports in the database
                var entries = airportCollection
                    .Where(q => !airfieldsInDb
                        .Select(w => w.OurAirfieldsId)
                        .Contains(q.OurAirfieldsId))
                    .ToList();

                var success = new List<Airfield>();
                var error = new List<Airfield>();

                //await context.BulkInsertAsync(entries.AsEnumerable());
                // I've been trying to use the Z.EntityFramework libraries, as a significant faster
                // way to do inserts, but individually inserting these items gives a better overview
                // over what's going wrong.

                foreach (var airfield in airportCollection
                    .Where(q => !airfieldsInDb
                        .Select(w => w.OurAirfieldsId)
                        .Contains(q.OurAirfieldsId)))
                {
                    try
                    {
                        using (var db = new BuggyDbContext())
                        {
                            db.Airfields.Add(airfield);
                            await db.SaveChangesAsync();
                            success.Add(airfield);
                        }
                    }
                    catch (Exception ex)
                    {
                        error.Add(airfield);
                    }
                }
            }
        }
    }
}
