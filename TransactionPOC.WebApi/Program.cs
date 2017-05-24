using Dapper;
using StructureMap;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using TransactionPOC.BLL.Services;
using TransactionPOC.Core.IoC;
using TransactionPOC.Core.Logging;
using TransactionPOC.DAL;
using TransactionPOC.WebApi.Controllers;
using TransactionPOC.WebApi.IoC;

namespace TransactionPOC.WebApi
{
    internal class Program
    {
        private static readonly ILogger Logger = LoggerFactory.Current.Create(MethodBase.GetCurrentMethod().DeclaringType);
        private static Container container;

        private static void Main(string[] args)
        {
            try
            {
                Bootstrap();

                var update2TablesController = container.GetInstance<Update2TablesController>();
                update2TablesController.Update2Tables();
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed updated 2 tables.", ex);
            }

            Logger.LogInfo("Done.");
            Console.ReadLine();
        }

        private static void Bootstrap()
        {
            container = new Container(new DefaultRegistry());
            ObjectFactory.Current = new StructureMapObjectFactory(container);
            var update2TablesService = container.GetInstance<IUpdate2TablesService>();
            SetupTables(container.GetInstance<UnitOfWork>());
        }

        private static void SetupTables(UnitOfWork unitOfWork)
        {
            var conn = unitOfWork.GetConnection();

            conn.Execute(@"use test;");

            new[] { "TableA", "TableB" }.ToList().ForEach(tblName => CreateTable(unitOfWork, tblName));
        }

        private static void CreateTable(UnitOfWork unitOfWork, string tableName)
        {
            var conn = unitOfWork.GetConnection();

            StringBuilder cmd = new StringBuilder()
                .Append($"if exists(select top 1 * from information_schema.tables where table_name='{tableName}')").AppendLine()
                .Append($"begin").AppendLine()
                .Append($"  drop table {tableName};").AppendLine()
                .Append($"end").AppendLine()
                .Append($"create table [{tableName}] (val datetime not null);");
            conn.Execute(cmd.ToString());
        }
    }
}