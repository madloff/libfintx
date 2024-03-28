﻿/*	
* 	
*  This file is part of libfintx.
*  
*  Copyright (C) 2018 Bjoern Kuensting
*  
*  This program is free software; you can redistribute it and/or
*  modify it under the terms of the GNU Lesser General Public
*  License as published by the Free Software Foundation; either
*  version 3 of the License, or (at your option) any later version.
*
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
*  Lesser General Public License for more details.
*
*  You should have received a copy of the GNU Lesser General Public License
*  along with this program; if not, write to the Free Software Foundation,
*  Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*  
*  Updates done by Torsten Klement <torsten.klinger@googlemail.com>
*  
*  Updates Copyright (c) 2024 Torsten Klement
* 	
*/

using libfintx.EBICS.Parameters;
using libfintx.EBICS.Responses;
using ebics = libfintx.Xsd.H004;

namespace libfintx.EBICS.Commands
{
    internal class HaaCommand : DCommand
    {
        internal HaaParams Params;
        protected override object _Params => new ebics.StandardOrderParamsType();

        protected override string SecurityMedium => Params.SecurityMedium;

        internal override string OrderType => "HAA";

        public HaaResponse Response = new HaaResponse();

        internal override DeserializeResponse Deserialize(string payload)
        {
            var ret = base.Deserialize(payload);
            UpdateResponse(Response, ret);
            Response.Data = XMLDeserialize<ebics.HAAResponseOrderDataType>(ResponseData);
            return ret;
        }

    }
}