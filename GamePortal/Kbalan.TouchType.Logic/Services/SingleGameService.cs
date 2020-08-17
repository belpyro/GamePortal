using AutoMapper;
using CSharpFunctionalExtensions;
using JetBrains.Annotations;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    class SingleGameService: ISingleGameService
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly TouchTypeGameContext _gameContext;

        public SingleGameService([NotNull]UserManager<ApplicationUser> userManager, [NotNull]IMapper mapper, [NotNull] TouchTypeGameContext gameContext)
        {
            _userManager = userManager;
            this._mapper = mapper;
            this._gameContext = gameContext;
        }

        public async Task<Result<NewSingleGameDto>> AddNewSingleGameAsync(string id)
        {
            try
            {
                var user = await _userManager.Users.Where(x => x.Id == id).Include("Setting")
                    .ProjectToSingleOrDefaultAsync<UserSettingDto>(_mapper.ConfigurationProvider)
                    .ConfigureAwait(false);
                var texts = await _gameContext.TextSets.Where(x => x.LevelOfText == user.Setting.LevelOfText)
                    .ToArrayAsync().ConfigureAwait(false);
                if (texts.Length == 0)
                {
                    return Result.Failure<NewSingleGameDto>($"No text set with level {user.Setting.LevelOfText} exists");
                }
               
                var text = _mapper.Map<TextSetDto>(texts.ElementAt(new Random().Next(0, texts.Length)));

                var resultDb = new SingleGame { 
                    TextForTyping = text.TextForTyping, 
                    CurrentPartToType = text.TextForTyping[0].ToString(),
                    UserId = user.Id,
                    SymbolsToType =text.TextForTyping.Length
                };

                _gameContext.SingleGames.Add(resultDb);
                await _gameContext.SaveChangesAsync().ConfigureAwait(false);
                var resultDto = new NewSingleGameDto
                {
                    Id = resultDb.Id,
                    TextForTyping = resultDb.TextForTyping
                };
                return Result.Success<NewSingleGameDto>(resultDto);
            }
            catch (SqlException ex)
            {
                return Result.Failure<NewSingleGameDto>(ex.Message);
            }
        }
    }
}
