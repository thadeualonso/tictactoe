using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace Tests
{
    public class GameManagerTests
    {
        [Test]
        public void check_if_start_game_is_called()
        {
            GameManager gameManager = Substitute.For<GameManager>();
        }
    }
}
