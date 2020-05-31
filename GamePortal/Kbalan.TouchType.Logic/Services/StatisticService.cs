using AutoMapper;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    class StatisticService : IStatisticService
    {
        private readonly TouchTypeGameContext _gameContext;
        private readonly IMapper _mapper;

        public StatisticService(TouchTypeGameContext gameContext, IMapper mapper)
        {
            this._gameContext = gameContext;
            this._mapper = mapper;
        }

        public IEnumerable<StatisticDto> GetAll()
        {
            return _gameContext.Statistics.ProjectToArray<StatisticDto>(_mapper.ConfigurationProvider);
        }

        public StatisticDto GetById(int id)
        {
            return _gameContext.Statistics.Where(x => x.StatisticId == id)
                   .ProjectToSingleOrDefault<StatisticDto>(_mapper.ConfigurationProvider);
        }

        public void Update(StatisticDto model)
        {
            var dbModel = _mapper.Map<StatisticDb>(model);
            _gameContext.Statistics.Attach(dbModel);
            var entry = _gameContext.Entry(dbModel);
            entry.Property(x => x.AvarageSpeed).IsModified = true;
            entry.Property(x => x.MaxSpeedRecord).IsModified = true;
            entry.Property(x => x.NumberOfGamesPlayed).IsModified = true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                _gameContext.Dispose();
                GC.SuppressFinalize(this);
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserService()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
