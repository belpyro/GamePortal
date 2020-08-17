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
    /// <summary>
    /// Service for single game methods
    /// </summary>
    class SingleGameService: ISingleGameService
    {
        //Constants for results of user turn
        const int INCORRECT = 0;
        const int CORRECT = 1;
        const int WIN = 2;

        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly TouchTypeGameContext _gameContext;

        public SingleGameService([NotNull]UserManager<ApplicationUser> userManager, [NotNull]IMapper mapper, [NotNull] TouchTypeGameContext gameContext)
        {
            _userManager = userManager;
            this._mapper = mapper;
            this._gameContext = gameContext;
        }

        /// <summary>
        /// Creating new game. First of all checking if user with such id is exists.
        /// Than checking if text with correct level is exists. If everything is ok
        /// new SingleGame is created in Db. TextForTyping - random text from Db
        /// which level is equal to user level. CurrentPartToType is equal to first symbol
        /// of text. UserId equal to user id. Symbols to type is equal to text lengh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<NewSingleGameDto>> AddNewSingleGameAsync(string id)
        {
            try
            {
                //find and check if user with such id is exists
                var user = await _userManager.Users.Where(x => x.Id == id).Include("Setting")
                    .ProjectToSingleOrDefaultAsync<UserSettingDto>(_mapper.ConfigurationProvider)
                    .ConfigureAwait(false);
                if (user == null)
                {
                    return Result.Failure<NewSingleGameDto>($"No user with such id {id} exists");
                }

                //find and check if text with user level is exists
                var texts = await _gameContext.TextSets.Where(x => x.LevelOfText == user.Setting.LevelOfText)
                    .ToArrayAsync().ConfigureAwait(false);
                if (texts.Length == 0)
                {
                    return Result.Failure<NewSingleGameDto>($"No text set with level {user.Setting.LevelOfText} exists");
                }
               
                var text = _mapper.Map<TextSetDto>(texts.ElementAt(new Random().Next(0, texts.Length)));

                //create new SingleGame and save it in Db
                var resultDb = new SingleGame { 
                    TextForTyping = text.TextForTyping, 
                    CurrentPartToType = text.TextForTyping[0].ToString(),
                    UserId = user.Id,
                    SymbolsToType =text.TextForTyping.Length
                };

                _gameContext.SingleGames.Add(resultDb);
                await _gameContext.SaveChangesAsync().ConfigureAwait(false);

                //create new NewSingleGameDto which contain text for typing and Single game id
                //and transfer it to client
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

        /// <summary>
        /// Handling of user's turn. Method receive the id of the game and string with 
        /// text, which user has typed + text which user has already succsefull typed 
        /// before. Method compare this text with text in "CurrentPartToType". If it's not 
        /// equal method return 0. If it equal and lenght of user text is equal to lenght of 
        /// whole text, this means that user has typed all symbols of the text sucssesfully
        /// and return 2(which means that game is finished) and delete text from db. In case
        /// if only text is equal method return 1. and save new data(SymbolTyped and CurrentPartToType)
        /// to db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="turn"></param>
        /// <returns></returns>
        public async Task<Result<SingleGameResultDto>> UserTurnAsync(int id, string turn)
        {
            try
            {
                //check if the game is exists
                var singleGame = await _gameContext.SingleGames.FirstOrDefaultAsync(x => x.Id == id);
                if(singleGame == null)
                {
                    return Result.Failure<SingleGameResultDto>($"No games with id {id} exists");
                }

                //check if text from client is equal to expected text
                if(singleGame.CurrentPartToType.Equals(turn))
                {
                    //increase sucsessfull typed symbol
                    singleGame.SymbolsTyped++;
                    //checking if symbols of client text is equal or not to whole text
                    if(singleGame.SymbolsTyped == singleGame.SymbolsToType)
                    {
                        //send finished game flag and delete game from db
                        singleGame.IsGameFinished = true;
                        _gameContext.SingleGames.Remove(singleGame);
                        await _gameContext.SaveChangesAsync();

                        return Result.Success(new SingleGameResultDto { TurnResult = WIN });
                    }
                    else
                    {
                        //send correct turn flag and save changes to(symbol typed and current text to type) db
                        singleGame.CurrentPartToType = singleGame.TextForTyping.Substring(0, singleGame.SymbolsTyped+1);
                        _gameContext.SingleGames.Attach(singleGame);
                        var entry = _gameContext.Entry(singleGame);
                        entry.Property(x => x.SymbolsTyped).IsModified = true;
                        entry.Property(x => x.CurrentPartToType).IsModified = true;
                        await _gameContext.SaveChangesAsync();
                        return Result.Success(new SingleGameResultDto { TurnResult = CORRECT });
                    }
                }
                else
                {
                    //send incorrect turn flag
                    return Result.Success(new SingleGameResultDto { TurnResult = INCORRECT });
                }
            }
            catch (SqlException ex)
            {
                return Result.Failure<SingleGameResultDto>(ex.Message);
            }


        }
    }
}
