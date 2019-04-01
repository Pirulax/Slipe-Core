﻿using Slipe.Client;
using Slipe.Client.Javascript;
using Slipe.Client.Enums;
using Slipe.Shared;
using RPCDefinitions;
using System;
using System.Diagnostics;
using System.Numerics;
using Slipe.Shared.Enums;
using Slipe.Shared.Structs;
using Slipe.Shared.RPC;
using Slipe.Client.Dx;
using Slipe.Client.Assets;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            new ElementManager(new ElementHelper());
            new Program();
        }

        private GUIBrowser guiBrowser;
        private Browser browser;

        public Program()
        {
            //guiBrowser = new GUIBrowser(Vector2.Zero, 1, 1, false, true, true);
            //browser = guiBrowser.Browser;

            //Browser.OnDomainRequestAccepted += OnDomainRequestAccepted;
            //browser.OnCreated += OnCreated;
            //browser.OnDocumentReady += OnReady;

            //browser.AddEventHandler("onClientBrowserCreated");
            //Camera.Instance.SetGoggleEffect(Slipe.Client.Enums.GoggleEffects.NORMAL);

            //RPCManager.Instance.RegisterRPC("testRPC", (TestRPCStruct arguments) => {
            //    Debug.WriteLine("Handling testRPC, name: {0}, x: {1}", arguments.name, arguments.x);
            //});
            RPCManager.Instance.RegisterRPC<BasicIncomingRPC>("testRPC", HandleTestRPC);
            RPCManager.Instance.TriggerRPC("onPlayerReady", new EmptyOutgoingRPC());

            Element dummy = new Element("flag", "ab3x");
            Debug.WriteLine(dummy.Type);


            new Mod("Assets/m4.txd", "Assets/m4.dff").Apply(356);

            Debug.WriteLine(Client.Renderer.Status.VideoCardName);
            Debug.WriteLine(Client.Renderer.Status.VideoCardRAM);

        }

        public void HandleTestRPC(BasicIncomingRPC arguments)
        {
            Debug.WriteLine("Handling testRPC, name: {0}, x: {1}, player name: {2}", arguments.name, arguments.x, ((Player)arguments.element).Name);
        }

        public void OnCreated()
        {
            Browser.RequestDomain("nanobob.net", false);
        }

        public void OnDomainRequestAccepted(string domain)
        {
            browser.LoadUrl("https://nanobob.net");
            Cursor.SetVisible(true);

            browser.AddEventHandler("onClientBrowserDocumentReady");
        }

        public void OnReady(string url)
        {
            browser.ExecuteJavascript("testMethod", new JavascriptVariable[]
            {
                new JavascriptVariable("test"),
                new JavascriptVariable(true),
                new JavascriptVariable(new string[]{
                    "hey", "there"
                }),
                new JavascriptVariable(5.2),
                new JavascriptVariable(10),
            });
        }
    }
}
