import unittest
from unittest.mock import patch
from your_module import main  # Replace 'your_module' with the actual module name

class TestMain(unittest.TestCase):

    @patch('dotenv.load_dotenv')
    def test_main_loads_environment_variables(self, mock_load_dotenv):
        # Arrange
        mock_load_dotenv.return_value = None
        
        # Act
        main()
        
        # Assert
        mock_load_dotenv.assert_called_once()

    @patch.dict(os.environ, {}, clear=True)
    def test_main_raises_exception_if_no_environment_variables_loaded(self):
        # Arrange
        pass
        
        # Act & Assert
        with self.assertRaises(EnvironmentError) as context:
            main()
        
        self.assertIn("Environment variables not loaded", str(context.exception))

if __name__ == '__main__':
    unittest.main()