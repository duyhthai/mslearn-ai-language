import unittest
from unittest.mock import patch
from io import StringIO

class TestMain(unittest.TestCase):
    @patch('dotenv.load_dotenv')
    def test_main_with_valid_env(self, mock_load_dotenv):
        # Arrange
        mock_load_dotenv.return_value = None
        os.environ['API_KEY'] = 'test_key'
        
        # Act
        from your_module_name import main  # Replace 'your_module_name' with the actual module name
        main()
        
        # Assert
        mock_load_dotenv.assert_called_once()
        self.assertIn('API_KEY', os.environ)
        self.assertEqual(os.environ['API_KEY'], 'test_key')

    @patch('dotenv.load_dotenv')
    def test_main_without_api_key(self, mock_load_dotenv):
        # Arrange
        mock_load_dotenv.return_value = None
        del os.environ['API_KEY']
        
        # Act
        from your_module_name import main  # Replace 'your_module_name' with the actual module name
        main()
        
        # Assert
        mock_load_dotenv.assert_called_once()
        self.assertNotIn('API_KEY', os.environ)

    @patch('dotenv.load_dotenv')
    def test_main_with_empty_api_key(self, mock_load_dotenv):
        # Arrange
        mock_load_dotenv.return_value = None
        os.environ['API_KEY'] = ''
        
        # Act
        from your_module_name import main  # Replace 'your_module_name' with the actual module name
        main()
        
        # Assert
        mock_load_dotenv.assert_called_once()
        self.assertIn('API_KEY', os.environ)
        self.assertEqual(os.environ['API_KEY'], '')

if __name__ == '__main__':
    unittest.main()