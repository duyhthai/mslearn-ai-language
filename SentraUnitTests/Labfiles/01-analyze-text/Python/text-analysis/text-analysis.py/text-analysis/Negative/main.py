import unittest
from unittest.mock import patch
import os

class TestMain(unittest.TestCase):
    @patch('os.getenv')
    def test_main_with_missing_env_var(self, mock_getenv):
        # Arrange
        mock_getenv.side_effect = KeyError
        
        # Act & Assert
        with self.assertRaises(KeyError):
            main()

    @patch('dotenv.load_dotenv')
    def test_main_with_load_failure(self, mock_load_dotenv):
        # Arrange
        mock_load_dotenv.return_value = None
        
        # Act & Assert
        with self.assertRaises(SystemExit) as context:
            main()
        self.assertEqual(context.exception.code, 1)

    @patch.dict(os.environ, {'API_KEY': 'invalid_key'})
    def test_main_with_invalid_api_key(self):
        # Arrange
        
        # Act & Assert
        with self.assertRaises(ValueError):
            main()

if __name__ == '__main__':
    unittest.main()