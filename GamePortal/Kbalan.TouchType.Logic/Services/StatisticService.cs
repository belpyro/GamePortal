using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<StatisticDto> _statisticValidator;

        public StatisticService(TouchTypeGameContext gameContext, IMapper mapper, IValidator<StatisticDto> StatisticValidator)
        {
            this._gameContext = gameContext;
            this._mapper = mapper;
            _statisticValidator = StatisticValidator;
        }

        /// <summary>
        /// Return all User with statistic from Db
        /// </summary>
        /// <returns>All Users with statistic</returns>
        public IEnumerable<UserStatisticDto> GetAll()
        {
            var models = _gameContext.Users.Include("Statistic").ToArray();
            return _mapper.Map<IEnumerable<UserStatisticDto>>(models);
        }

        /// <summary>
        /// Return User with it's statistic by user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user with statistic</returns>
        public UserStatisticDto GetById(int id)
        {
            return _gameContext.Users.Where(x => x.Id == id)
                   .ProjectToSingleOrDefault<UserStatisticDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Update existing user statistic
        /// </summary>
        /// <param name="id">user's id</param>
        /// <param name="model">new statistic model</param>
        public void Update(int id, StatisticDto model)
        {
            //Cheking if user with id exist
            var userModel = _gameContext.Users.Include("Statistic").SingleOrDefault(x => x.Id == id);
            if (userModel == null)
                throw new ArgumentNullException("No user with such Id exist");

            //Replace model setting id from Dto to correct id from Db and Valiate
            model.StatisticId = userModel.Statistic.StatisticId;
            _statisticValidator.ValidateAndThrow(model, "PostValidation");

            var modelDb = _mapper.Map<StatisticDb>(model);

            modelDb.StatisticId = userModel.Statistic.StatisticId;
            modelDb.User = userModel.Statistic.User;
            userModel.Statistic = modelDb;

            _gameContext.Users.Attach(userModel);
            _gameContext.SaveChanges();
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
