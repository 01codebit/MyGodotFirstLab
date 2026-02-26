using System.Collections.Generic;
using System.Text;
using Common;
using Godot;

public partial class Level : Node3D
{
	[Export]
	private string _baseUrl = "https://jsonplaceholder.typicode.com/";

	[Export]
	private string _endpoint = "todos";

	[Export]
	private int _userId = 1;

	[Export]
	private bool _completed = false;

	public override void _Ready()
	{
		// "https://api2.marittimi.dev.white3.it/v1/client/configuration"
		// "https://api.github.com/repos/godotengine/godot/releases/latest"
		// var apiUrl = "https://jsonplaceholder.typicode.com/todos?userId=1&completed=false";

		var sb = new StringBuilder();
		sb.Append(_baseUrl);
		if (!_baseUrl.EndsWith('/'))
			sb.Append("/");
		sb.Append(_endpoint);
		var apiUrl = sb.ToString();

		var parm = new Dictionary<string, string>();
		parm["userId"] = $"{_userId}";
		parm["completed"] = $"{_completed}".ToLower();

		apiUrl = GetRequestUri(apiUrl, parm);
		CustomLogger.Log($"[Level._Ready] apiUrl: {apiUrl}");

		HttpRequest httpRequest = GetNode<HttpRequest>("HTTPRequest");
		httpRequest.RequestCompleted += OnRequestCompleted;
		httpRequest.Request(apiUrl);
	}

	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		var str = Encoding.UTF8.GetString(body);
		GD.Print("[Level.OnRequestCompleted] body string: " + str);

		str = FixJson(str);

		Godot.Collections.Dictionary json = Json.ParseString(str).AsGodotDictionary();
		//GD.Print(json["name"]);
		GD.Print("[Level.OnRequestCompleted] body JSON: " + json);
	}

	public static string GetRequestUri(string endpoint, Dictionary<string, string> parameters)
	{
		if (string.IsNullOrEmpty(endpoint))
			return null;

		StringBuilder sb = new StringBuilder();
		sb.Append(endpoint);
		char separator = '?';
		//"todos?userId=1&completed=false"
		foreach (var key in parameters.Keys)
		{
			sb.Append(separator);
			sb.Append(key);
			sb.Append('=');
			sb.Append(parameters[key]);
			separator = '&';
		}

		return sb.ToString();
	}

	public static string FixJson(string value)
	{
		if (value.StartsWith('['))
		{
			value = "{\"Items\":" + value + "}";
		}

		return value;
	}
}
