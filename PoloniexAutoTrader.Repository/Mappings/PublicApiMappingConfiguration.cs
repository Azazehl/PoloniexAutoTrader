﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

using AutoMapper;

using PoloniexAutoTrader.Domain.Contracts;
using PoloniexAutoTrader.Domain.Models;
using PoloniexAutoTrader.Domain.Models.PublicApi;

using PoloniexAutoTrader.Repository.Dtos.PublicApi;


namespace PoloniexAutoTrader.Repository.Mappings {

  [Export(typeof(IAutoMapperConfiguration))]
  public sealed class PublicApiMappingConfiguration : IAutoMapperConfiguration {

    #region IAutoMapperConfiguration Implementation

    public void RegisterMappings(IMapperConfigurationExpression config) {
      config.CreateMap<KeyValuePair<string, TickerDto>, Ticker>().ForMember(dest => dest.CurrencyPair,  opt => opt.ResolveUsing(src => new CurrencyPair(src.Key)))
                                                                 .ForMember(dest => dest.Id,            opt => opt.MapFrom(src => src.Value.Id))
                                                                 .ForMember(dest => dest.Last,          opt => opt.ResolveUsing(src => Double.Parse(src.Value.Last)))
                                                                 .ForMember(dest => dest.LowestAsk,     opt => opt.ResolveUsing(src => Double.Parse(src.Value.LowestAsk)))
                                                                 .ForMember(dest => dest.HighestBid,    opt => opt.ResolveUsing(src => Double.Parse(src.Value.HighestBid)))
                                                                 .ForMember(dest => dest.PercentChange, opt => opt.ResolveUsing(src => Double.Parse(src.Value.PercentChange)))
                                                                 .ForMember(dest => dest.BaseVolume,    opt => opt.ResolveUsing(src => Double.Parse(src.Value.BaseVolume)))
                                                                 .ForMember(dest => dest.QuoteVolume,   opt => opt.ResolveUsing(src => Double.Parse(src.Value.QuoteVolume)))
                                                                 .ForMember(dest => dest.IsFrozen,      opt => opt.ResolveUsing(src => src.Value.IsFrozen == "1"))
                                                                 .ForMember(dest => dest.High24Hour,    opt => opt.ResolveUsing(src => Double.Parse(src.Value.High24hr)))
                                                                 .ForMember(dest => dest.Low24Hour,     opt => opt.ResolveUsing(src => Double.Parse(src.Value.Low24hr)))
                                                                 .ForMember(dest => dest.Currency,      opt => opt.Ignore());

      config.CreateMap<KeyValuePair<string, CurrencyDto>, Currency>().ForMember(dest => dest.Name,           opt => opt.MapFrom(src => src.Key))
                                                                     .ForMember(dest => dest.Id,             opt => opt.MapFrom(src => src.Value.Id))
                                                                     .ForMember(dest => dest.Description,    opt => opt.MapFrom(src => src.Value.Name))
                                                                     .ForMember(dest => dest.TransactionFee, opt => opt.MapFrom(src => Double.Parse(src.Value.TxFee)))
                                                                     .ForMember(dest => dest.MinimumConf,    opt => opt.MapFrom(src => src.Value.MinConf))
                                                                     .ForMember(dest => dest.DepositAddress, opt => opt.MapFrom(src => src.Value.DepositAddress))
                                                                     .ForMember(dest => dest.IsDisabled,     opt => opt.MapFrom(src => src.Value.Disabled == 1))
                                                                     .ForMember(dest => dest.IsDelisted,     opt => opt.MapFrom(src => src.Value.Delisted == 1))
                                                                     .ForMember(dest => dest.IsFrozen,       opt => opt.MapFrom(src => src.Value.Frozen == 1));
    }

    #endregion IAutoMapperConfiguration Implementation

  }

}
