﻿// $begin{copyright}
//
// This file is confidential and proprietary.
//
// Copyright (c) IntelliFactory, 2004-2009.
//
// All rights reserved.  Reproduction or use in whole or in part is
// prohibited without the written consent of the copyright holder.
//-----------------------------------------------------------------
// $end{copyright}

namespace IntelliFactory.WebSharper.JQueryUI

/// Contains YUI resource descriptors and their dependency information.
module Dependencies =
    open IntelliFactory.WebSharper
    open System.Configuration

    module Utils = 
        let JQueryForUIBase =
            match ConfigurationManager.AppSettings.["IntelliFactory.WebSharper.JQuery"] with
            | null ->
                "http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"
            | url ->
                url


        let JQueryUIBase =
            match ConfigurationManager.AppSettings.["IntelliFactory.WebSharper.JQueryUI"] with
            | null ->
                "http://jquery-ui.googlecode.com/svn/tags/1.8rc3/ui"
            | url ->
                url

        let JQueryUIBaseCss =
            match ConfigurationManager.AppSettings.["IntelliFactory.WebSharper.JQueryUICss"] with
            | null ->
                "http://jquery-ui.googlecode.com/svn/tags/1.8rc3/themes/base"
            | url ->
                url     
        
        [<AbstractClass>]
        type Module(name: string) =
            interface IResource with            
                member this.Render(resolver, writer) =
                    let loc =
                        sprintf "%s/%s.js" JQueryUIBase name
                    Resource.RenderJavaScript loc writer

        [<AbstractClass>]
        type ModuleCss(name: string) =
            interface IResource with
                member this.Render(resolver, writer) =
                    let loc = 
                        sprintf "%s/%s.css" JQueryUIBaseCss name
                    Resource.RenderCss loc writer

    open Utils

    type JQueryBase() =
        interface IResource with
            member this.Render(resolver, writer) =
                Resource.RenderJavaScript JQueryForUIBase writer

    and JQueryUIAll() = 
        inherit Module("jquery-ui")
                
    and AllCss()= 
        inherit ModuleCss("jquery.ui.all")