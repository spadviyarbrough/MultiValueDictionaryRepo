using Microsoft.Extensions.Logging;
using Moq;
using MultiValueDictionary;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using ILogger = Serilog.ILogger;

namespace MultiValueDictionaryTest
{
    public class GenericMultiValueDictionaryTest
    {
        private ILogger<GenericMultiValueDictionary<string,string>> _logger;
        private ILogger<GenericMultiValueDictionary<int, int>> _intlogger;
        private readonly ITestOutputHelper output;
        public GenericMultiValueDictionaryTest(ITestOutputHelper output)
        {
            this.output = output;
            _logger = new XunitLogger<GenericMultiValueDictionary<string, string>>(output);
            _intlogger = new XunitLogger<GenericMultiValueDictionary<int, int>>(output);
        }


        [Fact]
        public void ShouldAddValue()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            var actualOutput = service.Add("foo", "bar");
            Assert.True(actualOutput);
        }

        [Fact]
        public void ShouldNotAddValue()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            service.Add("foo", "bar");
            var actualOutput = service.Add("foo", "bar");
            Assert.False(actualOutput);
        }


        [Fact]
        public void ShouldReturnValueForGetMembers()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            service.Add("foo", "bar");
            List<string> expectedOutput = new List<string> { "bar" };
            var actualOutput = service.GetMembers("foo");
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact]
        public void ShouldReturnEmptyValuesForGetMemebers()
        {

            var service = new GenericMultiValueDictionary<string, string>(_logger);
            var actual = service.GetMembers("bad");
            Assert.Empty(actual);

        }

        
        [Fact]
        public void ShouldReturnKeys()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            service.Add("foo", "bar");
            service.Add("baz", "bang");
            var expected = new List<string> { "foo", "baz" };
            var actual = service.GetKeys();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShoultNotReturnKey()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            var actual = service.GetKeys();
            Assert.Empty(actual);
        }

        [Fact]
        public void ShouldRemoveMember()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            service.Add("foo", "bar");
            var actual = service.RemoveMember("foo", "bar");
            Assert.True(actual);

        }

        [Fact]
        public void ShouldRemoveKeyForLastMember()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            service.Add("foo", "bar");
            service.RemoveMember("foo", "bar");
            var actual = service.GetKeys();
            Assert.Empty(actual);

        }

        [Fact]
        public void ShouldHandleAlreadyForRemoveMember()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            service.Add("foo", "bar");
            service.Add("foo", "baz");
            service.RemoveMember("foo", "bar");
            var actual = service.RemoveMember("foo", "bar");
            Assert.False(actual);
        }

        [Fact]
        public void ShouldHandleNonExistingKeyForRemoveMember()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            var actual = service.RemoveMember("foo", "bar");
            Assert.False(actual);
        }

        [Fact]
        public void ShouldRemoveAllForKey()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            service.Add("foo", "bar");
            service.Add("foo", "baz");
            var actual = service.RemoveKey("foo");
            Assert.True(actual);
        }

        [Fact]
        public void ShouldHandleNonExistingKeyForRemoveKey()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            var actual = service.RemoveKey("foo");
            Assert.False(actual);
        }

        [Fact]
        public void ShouldClearDictionary()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            service.Add("foo", "bar");
            service.Add("bang", "zip");
            service.ClearAll();
            var actual = service.GetKeys();
            Assert.Empty(actual);

        }

        [Fact]
        public void ShouldHandleExistingKeyForKeyExists()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            service.Add("foo", "bar");
            var actual = service.KeyExists("foo");
            Assert.True(actual);
        }

        [Fact]
        public void ShouldHanleNonExistingKeyForKeyExists()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            var actual = service.KeyExists("foo");
            Assert.False(actual);
        }

        [Fact]
        public void ShouldHandleNonExistingKeyForMemberExists()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            var actual = service.MemberExists("foo", "bar");
            Assert.False(actual);
        }

        [Fact]
        public void ShouldHandleExistingKeyForMemberExists()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            service.Add("foo", "bar");
            var actual = service.MemberExists("foo", "bar");
            Assert.True(actual);
        }

        [Fact]
        public void ShouldNotReturnAllMembers()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            var actual = service.GetAllMembers();
            Assert.Empty(actual);

        }

        [Fact]
        public void ShouldReturnAllMembers()
        {
            var service = new GenericMultiValueDictionary<string, string>(_logger);
            service.Add("foo", "bar");
            service.Add("foo", "baz");
            var actual = service.GetAllMembers();
            var expected = new List<string> { "bar","baz"};
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldWorkWithIntAddValueGetMembers()
        {
            var service = new GenericMultiValueDictionary<int, int>(_intlogger);
            service.Add(3, 1);
            service.Add(3, 2);
            var actual = service.GetAllMembers();
            var expected = new List<int> { 1,2};
            Assert.Equal(expected, actual);
        }

        
    }
}
