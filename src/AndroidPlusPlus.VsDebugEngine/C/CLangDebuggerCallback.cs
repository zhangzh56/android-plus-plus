﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Microsoft.VisualStudio.Debugger.Interop;

using AndroidPlusPlus.Common;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace AndroidPlusPlus.VsDebugEngine
{

  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public delegate int CLangDebuggerEventDelegate (CLangDebugger debugger);

  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public interface CLangDebuggerEventInterface
  {
    int OnStartServer (CLangDebugger debugger);

    int OnTerminateServer (CLangDebugger debugger);

    int OnAttachClient (CLangDebugger debugger);

    int OnDetachClient (CLangDebugger debugger);

    int OnStopClient (CLangDebugger debugger);

    int OnContinueClient (CLangDebugger debugger);

    int OnTerminateClient (CLangDebugger debugger);
  }

  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  public class CLangDebuggerCallback : CLangDebuggerEventInterface, IDebugEventCallback2
  {

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private readonly DebugEngine m_debugEngine;

    private readonly Dictionary<Guid, CLangDebuggerEventDelegate> m_debuggerCallback;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public CLangDebuggerCallback (DebugEngine engine)
    {
      m_debugEngine = engine;

      // 
      // Register function handlers for specific events.
      // 

      m_debuggerCallback = new Dictionary<Guid, CLangDebuggerEventDelegate> ();

      m_debuggerCallback.Add (ComUtils.GuidOf (typeof (CLangDebuggerEvent.StartServer)), OnStartServer);

      m_debuggerCallback.Add (ComUtils.GuidOf (typeof (CLangDebuggerEvent.TerminateServer)), OnTerminateServer);

      m_debuggerCallback.Add (ComUtils.GuidOf (typeof (CLangDebuggerEvent.AttachClient)), OnAttachClient);

      m_debuggerCallback.Add (ComUtils.GuidOf (typeof (CLangDebuggerEvent.DetachClient)), OnDetachClient);

      m_debuggerCallback.Add (ComUtils.GuidOf (typeof (CLangDebuggerEvent.StopClient)), OnStopClient);

      m_debuggerCallback.Add (ComUtils.GuidOf (typeof (CLangDebuggerEvent.ContinueClient)), OnContinueClient);

      m_debuggerCallback.Add (ComUtils.GuidOf (typeof (CLangDebuggerEvent.TerminateClient)), OnTerminateClient);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public bool IsRegistered (ref Guid riidEvent)
    {
      return m_debuggerCallback.ContainsKey (riidEvent);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int Event (IDebugEngine2 pEngine, IDebugProcess2 pProcess, IDebugProgram2 pProgram, IDebugThread2 pThread, IDebugEvent2 pEvent, ref Guid riidEvent, uint dwAttrib)
    {
      // 
      // Custom event handler.
      // 

      try
      {
        LoggingUtils.Print ("[CLangDebuggerCallback] Event: " + riidEvent.ToString ());

        CLangDebuggerEventDelegate eventCallback;

        if (!m_debuggerCallback.TryGetValue (riidEvent, out eventCallback))
        {
          return DebugEngineConstants.E_NOTIMPL;
        }

        return eventCallback (m_debugEngine.NativeDebugger);
      }
      catch (Exception e)
      {
        LoggingUtils.HandleException (e);

        throw;
      }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int OnStartServer (CLangDebugger debugger)
    {
      LoggingUtils.PrintFunction ();

      try
      {
        debugger.GdbServer.Start ();

        return DebugEngineConstants.S_OK;
      }
      catch (Exception e)
      {
        LoggingUtils.HandleException (e);

        throw;
      }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int OnTerminateServer (CLangDebugger debugger)
    {
      LoggingUtils.PrintFunction ();

      try
      {
        debugger.GdbServer.Kill ();

        return DebugEngineConstants.S_OK;
      }
      catch (Exception e)
      {
        LoggingUtils.HandleException (e);

        throw;
      }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int OnAttachClient (CLangDebugger debugger)
    {
      LoggingUtils.PrintFunction ();

      try
      {
        GdbServer gdbServer = debugger.GdbServer;

        debugger.GdbClient.Attach (gdbServer);

        return DebugEngineConstants.S_OK;
      }
      catch (Exception e)
      {
        LoggingUtils.HandleException (e);

        throw;
      }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int OnDetachClient (CLangDebugger debugger)
    {
      LoggingUtils.PrintFunction ();

      try
      {
        bool shouldContinue = false;

        ManualResetEvent detachLock = new ManualResetEvent (false);

        debugger.RunInterruptOperation (delegate (CLangDebugger _debugger)
        {
          _debugger.GdbClient.Detach ();

          detachLock.Set ();
        }, shouldContinue);

        bool detachedSignaled = detachLock.WaitOne (1000);

        if (!detachedSignaled)
        {
          throw new InvalidOperationException ("Failed to detach GDB client");
        }

        return DebugEngineConstants.S_OK;
      }
      catch (Exception e)
      {
        LoggingUtils.HandleException (e);

        throw;
      }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int OnStopClient (CLangDebugger debugger)
    {
      LoggingUtils.PrintFunction ();

      try
      {
        debugger.GdbClient.Stop ();

        return DebugEngineConstants.S_OK;
      }
      catch (Exception e)
      {
        LoggingUtils.HandleException (e);

        throw;
      }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int OnContinueClient (CLangDebugger debugger)
    {
      LoggingUtils.PrintFunction ();

      try
      {
        debugger.GdbClient.Continue ();

        return DebugEngineConstants.S_OK;
      }
      catch (Exception e)
      {
        LoggingUtils.HandleException (e);

        throw;
      }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int OnTerminateClient (CLangDebugger debugger)
    {
      LoggingUtils.PrintFunction ();

      try
      {
        debugger.GdbClient.Stop ();

        debugger.GdbClient.Terminate ();

        return DebugEngineConstants.S_OK;
      }
      catch (Exception e)
      {
        LoggingUtils.HandleException (e);

        return DebugEngineConstants.E_FAIL;
      }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  }

  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
