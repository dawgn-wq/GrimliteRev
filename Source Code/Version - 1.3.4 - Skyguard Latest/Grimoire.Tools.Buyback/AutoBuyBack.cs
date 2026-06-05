using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Grimoire.Tools.Buyback;

public class AutoBuyBack : IDisposable
{
	private const string UrlBuyBack = "inventory.aspx?tab=buyback";

	private readonly HttpClient _client;

	protected internal string Username => Flash.Call<string>("Username", new string[0]);

	protected internal string Password => Flash.Call<string>("Password", new string[0]);

	public AutoBuyBack()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		_client = new HttpClient((HttpMessageHandler)new HttpClientHandler
		{
			AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate),
			CookieContainer = new CookieContainer()
		})
		{
			BaseAddress = new Uri("https://account.aq.com")
		};
	}

	public async Task Perform(string item, int pageCap)
	{
		bool flag = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
		bool flag2 = flag;
		string lastRequestedPage = default(string);
		if (flag2)
		{
			flag2 = !string.IsNullOrEmpty(lastRequestedPage = await SendPost(string.Empty, "uuu=" + Username + "&pps=" + Password + "&submit="));
		}
		bool flag3 = flag2;
		string[] array = default(string[]);
		if (flag3)
		{
			string[] array2;
			array = (array2 = await GetItemHtml(lastRequestedPage, item, pageCap));
			flag3 = array2.Length >= 2;
		}
		if (flag3)
		{
			BuyBackPage buyBackPage = new BuyBackPage(array[0]);
			BuyBackPage buyBackPage2 = new BuyBackPage(array[1]);
			string postData = "__EVENTTARGET=GridBuyBack&__EVENTARGUMENT=" + buyBackPage.EventArgument + "&__VIEWSTATE=" + buyBackPage2.ViewState + "&__VIEWSTATEGENERATOR=" + buyBackPage2.ViewStateGenerator + "&__VIEWSTATEENCRYPTED=&__EVENTVALIDATION=" + buyBackPage2.EventValidation;
			string html;
			if (!string.IsNullOrEmpty(html = await SendPost("inventory.aspx?tab=buyback", postData)))
			{
				BuyBackPage buyBackPage3 = new BuyBackPage(html);
				string postData2 = "__VIEWSTATE=" + buyBackPage3.ViewState + "&__VIEWSTATEGENERATOR=" + buyBackPage3.ViewStateGenerator + "&__VIEWSTATEENCRYPTED=&__EVENTVALIDATION=" + buyBackPage3.EventValidation + "&btnConfirmYes=YES%2c+GET+NOW+FOR+FREE";
				await SendPost("inventory.aspx?tab=buyback", postData2);
			}
		}
	}

	private async Task<string[]> GetItemHtml(string lastRequestedPage, string item, int cap)
	{
		string[] ret = new string[2];
		for (int i = 1; i <= cap; i++)
		{
			BuyBackPage buyBackPage = new BuyBackPage(lastRequestedPage);
			string postData = string.Format("__EVENTTARGET={0}&__EVENTARGUMENT=Page%24{1}&", "GridBuyBack", i) + "__VIEWSTATE=" + buyBackPage.ViewState + "&__VIEWSTATEGENERATOR=" + buyBackPage.ViewStateGenerator + "&__VIEWSTATEENCRYPTED=&__EVENTVALIDATION=" + buyBackPage.EventValidation;
			string text = await SendPost("inventory.aspx?tab=buyback", postData);
			lastRequestedPage = text;
			string[] array = text.Split('\n');
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				if (text2.IndexOf(item, StringComparison.OrdinalIgnoreCase) > -1)
				{
					ret[0] = text2;
					ret[1] = text;
					return ret;
				}
			}
		}
		return ret;
	}

	private async Task<string> SendPost(string url, string postData)
	{
		try
		{
			return HttpUtility.HtmlDecode(await _client.PostAsync(url, (HttpContent)new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded")).Result.Content.ReadAsStringAsync());
		}
		catch
		{
			return string.Empty;
		}
	}

	private async Task<string> SendGet(string url)
	{
		try
		{
			return HttpUtility.HtmlDecode(await _client.GetStringAsync(url));
		}
		catch
		{
			return string.Empty;
		}
	}

	public void Dispose()
	{
		SendGet("logout.aspx").Wait();
		((HttpMessageInvoker)_client).Dispose();
	}

	void IDisposable.Dispose()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Dispose
		this.Dispose();
	}
}
