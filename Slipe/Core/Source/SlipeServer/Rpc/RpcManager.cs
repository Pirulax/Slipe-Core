﻿using Slipe.MTADefinitions;
using System;
using System.Collections.Generic;
using System.Text;
using Slipe.Shared.Elements;
using Slipe.Server.Peds;

namespace Slipe.Server.Rpc
{
    /// <summary>
    /// Manager class that handles RPC's between server and clients
    /// </summary>
    public class RpcManager
    {
        private static RpcManager instance;
        public static RpcManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RpcManager();
                }
                return instance;
            }
        }

        private Dictionary<string, Action<Player, object>> RegisteredRPCs;

        private RpcManager()
        {
            RegisteredRPCs = new Dictionary<string, Action<Player, object>>();

            Element.OnRootEvent += (eventName, source, p1, p2, p3, p4, p5, p6, p7, p8) =>
            {
                if (RegisteredRPCs.ContainsKey(eventName))
                {
                    Element element = ElementManager.Instance.GetElement(source);
                    Player player = (Player)element;
                    RegisteredRPCs[eventName].Invoke(player, p1);
                }
            };
        }

        /// <summary>
        /// Register an RPC
        /// </summary>
        public void RegisterRPC<CallbackType>(string key, Action<Player, CallbackType> callback)
        {
            RegisteredRPCs[key] = (player, parameters) =>
            {
                /*
                [[
                    callback(player, CallbackType(parameters))
                ]]
                 */
            };
            MTAShared.AddEvent(key, true);
            Element.Root.AddEventHandler(key);
        }

        /// <summary>
        /// Trigger an RPC on a player
        /// </summary>
        public void TriggerRPC(Player target, string key, object argument)
        {
            MTAServer.TriggerClientEvent(target.MTAElement, key, Element.Root.MTAElement, argument);
        }

        /// <summary>
        /// Trigger an RPC
        /// </summary>
        public void TriggerRPC(string key, object argument)
        {
            MTAServer.TriggerClientEvent(Element.Root.MTAElement, key, Element.Root.MTAElement, argument);
        }
    }
}