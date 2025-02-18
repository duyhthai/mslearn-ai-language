import unittest
from unittest.mock import patch
from your_module import main  # Replace 'your_module' with the actual module name

class TestMain(unittest.TestCase):

    @patch.dict('os.environ', {'KEY': 'value'}, clear=True)
    def test_main_with_environment_variable(self):
        main()
        # Add assertions here to verify the expected behavior when environment variable is set

    @patch.dict('os.environ', {}, clear=True)
    def test_main_without_environment_variable(self):
        with self.assertRaises(KeyError):
            main()
        # Add assertions here to verify the expected behavior when environment variable is not set

if __name__ == '__main__':
    unittest.main()