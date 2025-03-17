import unittest
from unittest.mock import patch
from your_module import main  # Replace 'your_module' with the actual module name

class TestMain(unittest.TestCase):

    @patch('os.environ.get')
    def test_main_with_empty_env(self, mock_get):
        mock_get.return_value = None
        with self.assertRaises(Exception):
            main()

    @patch.dict(os.environ, {'KEY': 'VALUE'})
    def test_main_with_valid_env(self, mock_environ):
        with patch('dotenv.load_dotenv') as mock_load_dotenv:
            main()
            mock_load_dotenv.assert_called_once()

    @patch('os.environ.get')
    def test_main_with_missing_key(self, mock_get):
        mock_get.side_effect = KeyError('Missing key')
        with self.assertRaises(KeyError):
            main()

if __name__ == '__main__':
    unittest.main()