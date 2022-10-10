﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpotifyApi.Models;
using SpotifyApi.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        private readonly ISpotifyService _spotifyService;
        public HomeController(ILogger<HomeController> logger,
            ISpotifyAccountService spotifyAccountService, IConfiguration configuration, ISpotifyService spotifyService)
        {
            _logger = logger;
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> Index()
        {
            var newReleases = await GetReleases();

            return View(newReleases);
        }

        private async Task<IEnumerable<Release>> GetReleases()
        {
            try
            {
                var token = await _spotifyAccountService.GetToken(
                    _configuration["Spotify:ClientId"],
                    _configuration["Spotify:ClientSecret"]);

                var newReleases = await _spotifyService.GetNewReleases("TR", 20, token);

                return newReleases;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);

                return Enumerable.Empty<Release>();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
