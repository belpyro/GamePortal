using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using JetBrains.Annotations;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly TouchTypeGameContext _gameContext;
        private readonly IMapper _mapper;
        private readonly IValidator<StatisticDto> _statisticValidator;

        public StatisticService([NotNull]TouchTypeGameContext gameContext, [NotNull]IMapper mapper, [NotNull]IValidator<StatisticDto> StatisticValidator)
        {
            this._gameContext = gameContext;
            this._mapper = mapper;
            _statisticValidator = StatisticValidator;
        }

        /// <summary>
        /// Return all User with statistic from Db
        /// </summary>
        /// <returns>All Users with statistic</returns>
        public Result<IEnumerable<UserStatisticDto>> GetAll()
        {
            try
            {
                var models = _gameContext.Users.Include("Statistic").ToArray();
                return Result.Success<IEnumerable<UserStatisticDto>>(_mapper.Map<IEnumerable<UserStatisticDto>>(models));
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<IEnumerable<UserStatisticDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Return User with it's statistic by user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user with statistic</returns>
        public Result<UserStatisticDto> GetById(int id)
        {
            try
            {
                var getResultById = _gameContext.Users.Where(x => x.Id == id)
                    .ProjectToSingleOrDefault<UserStatisticDto>(_mapper.ConfigurationProvider);

                if (getResultById != null)
                    return Result.Success<UserStatisticDto>(getResultById);

                return Result.Failure<UserStatisticDto>("No user with such id exist");
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<UserStatisticDto>(ex.Message);
            }
        }

        /// <summary>
        /// Update existing user statistic
        /// </summary>
        /// <param name="id">user's id</param>
        /// <param name="model">new statistic model</param>
        public Result Update(int id, StatisticDto model)
        {
            //Cheking if user with id exist
            var userModel = _gameContext.Users.Include("Statistic").SingleOrDefault(x => x.Id == id);

            //Replace model statistic id from Dto to correct id from Db and Valiate
            model.StatisticId = userModel.Statistic.StatisticId;
            try
            {
                var modelDb = _mapper.Map<StatisticDb>(model);

                modelDb.StatisticId = userModel.Statistic.StatisticId;
                modelDb.User = userModel.Statistic.User;
                userModel.Statistic = modelDb;

                _gameContext.Users.Attach(userModel);
                _gameContext.SaveChanges();

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure(ex.Message);
            }
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
