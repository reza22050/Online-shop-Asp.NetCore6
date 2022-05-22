using Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest1.Builders
{
    public class AddressBuilder
    {
        private Address _address;
        public string TestCity => "Tehran";
        public string TestState => "Valiasr";
        public string TestZipCode => "5484285645";
        public string TestPostalAddress => "Negar Tower";

        public AddressBuilder()
        {
            _address = WithDefaultValues();
        }
        public Address Build()
        {
            return _address;
        }

        private Address WithDefaultValues()
        {
            _address = new Address(TestState, TestCity, TestZipCode, TestPostalAddress, "test");
            return _address; 
        }


    }
}
