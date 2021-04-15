using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InternshipClass.Tests
{
    public class StartupTests
    {
        [Fact]
        public void ShouldConvertDatabaseUrlToHerokuString()
        {
            //Assume
            string url = "postgres://cueheijfcjxkbc:c2417b22d39b9642df0d8ac9c6921fcfce37968b2248ee6caefee8621745b247@ec2-99-80-200-225.eu-west-1.compute.amazonaws.com:5432/d9bnpori3022u7";

            //Act
            var herokuConnectionString = Startup.ConvertDatabaseUrlToHerokuString(url);

            //Assert
            Assert.Equal("Server=ec2-99-80-200-225.eu-west-1.compute.amazonaws.com;Port=5432;Database=d9bnpori3022u7;User Id=cueheijfcjxkbc;Password=c2417b22d39b9642df0d8ac9c6921fcfce37968b2248ee6caefee8621745b247;Pooling=true;SSL Mode=Require;Trust Server Certificate=True;", herokuConnectionString);
        }
    }
}
