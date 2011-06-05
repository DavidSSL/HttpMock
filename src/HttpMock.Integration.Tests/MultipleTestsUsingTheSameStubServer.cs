﻿using System.Net;
using HttpMock;
using NUnit.Framework;

namespace StubHttp
{
	[TestFixture]
	public class MultipleTestsUsingTheSameStubServer
	{
		private IHttpEndpoint _httpEndpoint;

		[TestFixtureSetUp]
		public void SetUp() {
			_httpEndpoint = new HttpEndpoint().At("http://localhost:8081/");
		}

		[Test]
		public void FirstTest() {
			var wc = new WebClient();
			string stubbedReponse = "Response for first test";
			_httpEndpoint.WithNewContext()
				.Stub(x => x.Post("/firsttest"))
				.Return(stubbedReponse)
				.OK();

			Assert.That(wc.DownloadString("Http://localhost:8081/firsttest/"), Is.EqualTo(stubbedReponse));
		}

		[Test]
		public void SecondTest()
		{
			var wc = new WebClient();
			string stubbedReponse = "Response for second test";
			_httpEndpoint.WithNewContext()
				.Stub(x => x.Post("/secondtest"))
				.Return(stubbedReponse).OK();

			Assert.That(wc.DownloadString("Http://localhost:8081/secondtest/"), Is.EqualTo(stubbedReponse));
		}
		
		[TestFixtureTearDown]
		public void TearDown() {
			_httpEndpoint.Dispose();
		}
	}
}