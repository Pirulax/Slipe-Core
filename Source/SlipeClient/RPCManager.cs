﻿using Slipe.Shared;
using Slipe.MTADefinitions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Slipe.Client
{
    public class RPCManager
    {
        private static RPCManager instance;
        public static RPCManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RPCManager();
                }
                return instance;
            }
        }
        
        private Dictionary<string, Action<object>> RegisteredRPCs;

        private RPCManager()
        {
            RegisteredRPCs = new Dictionary<string, Action<object>>();

            Element.OnRootEvent += (string eventName, MTAElement source, dynamic p1, dynamic p2, dynamic p3, dynamic p4, dynamic p5, dynamic p6, dynamic p7, dynamic p8) =>
            {
                Console.WriteLine("CLIENT RPC TRIGGER");
                if (RegisteredRPCs.ContainsKey(eventName))
                {
                    RegisteredRPCs[eventName].Invoke(p1);
                }
            };
        }

        /// <summary>
        /// Register an RPC
        /// </summary>
        public void RegisterRPC<CallbackType>(string key, Action<CallbackType> callback)
        {
            RegisteredRPCs[key] = (object parameters) => {
                /*
                [[
                    callback(CallbackType(parameters))
                ]]
                 */
            };
            MTAShared.AddEvent(key, true);
            Element.Root.AddEventHandler(key);
        }

        /// <summary>
        /// Trigger an RPC
        /// </summary>
        public void TriggerRPC(string key, object argument)
        {
            MTAClient.TriggerServerEvent(key, Element.Root.MTAElement, argument);
        }
    }
}