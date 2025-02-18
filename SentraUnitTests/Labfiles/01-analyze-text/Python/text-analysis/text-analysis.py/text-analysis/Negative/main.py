import unittest
from unittest.mock import patch
from io import StringIO
import sys

class TestMain(unittest.TestCase):
    @patch('dotenv.load_dotenv')
    def test_main_no_env_file(self, mock_load_dotenv):
        # Arrange
        mock_load_dotenv.return_value = None
        
        # Redirect stdout to capture print statements
        original_stdout = sys.stdout
        sys.stdout = new_stdout = StringIO()
        
        # Act
        main()
        
        # Assert
        captured_output = new_stdout.getvalue().strip()
        self.assertIn("Error: .env file not found", captured_output)
        
        # Cleanup
        sys.stdout = original_stdout

    @patch.dict(os.environ, {'API_KEY': 'invalid_key'})
    def test_main_invalid_api_key(self):
        # Arrange
        
        # Redirect stdout to capture print statements
        original_stdout = sys.stdout
        sys.stdout = new_stdout = StringIO()
        
        # Act
        main()
        
        # Assert
        captured_output = new_stdout.getvalue().strip()
        self.assertIn("Error: Invalid API key", captured_output)
        
        # Cleanup
        sys.stdout = original_stdout

    @patch.dict(os.environ, {'API_KEY': ''})
    def test_main_empty_api_key(self):
        # Arrange
        
        # Redirect stdout to capture print statements
        original_stdout = sys.stdout
        sys.stdout = new_stdout = StringIO()
        
        # Act
        main()
        
        # Assert
        captured_output = new_stdout.getvalue().strip()
        self.assertIn("Error: API key cannot be empty", captured_output)
        
        # Cleanup
        sys.stdout = original_stdout

if __name__ == '__main__':
    unittest.main()