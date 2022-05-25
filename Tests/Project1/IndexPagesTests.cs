using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Connection;
using Project_1.Data.Party;
using Project_1.Domain.Connection;

namespace Project_1.Tests.Project1;

public abstract class PagesTests : HostTests { } 
[TestClass] public class IndexPagesTests: PagesTests { }
