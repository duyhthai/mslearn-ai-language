import unittest
from unittest.mock import patch
from your_module import main  # Replace 'your_module' with the actual module name where main is defined

class TestMain(unittest.TestCase):
    @patch('dotenv.load_dotenv')
    def test_main_with_valid_env(self, mock_load_dotenv):
        # Arrange
        mock_load_dotenv.return_value = None
        
        # Act
        result = main()
        
        # Assert
        mock_load_dotenv.assert_called_once()

if __name__ == '__main__':
    unittest.main()