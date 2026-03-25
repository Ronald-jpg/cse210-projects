import random
import string
chars = string.punctuation + string.digits + string.ascii_letters + ""
chars = list(chars)
keys = chars.copy()
random.shuffle(keys)
a = ["Roni","Brian","Elly","Danika"]
print(random.choice(a))
print(chars)
print(keys)

#Encryption
plain_text  = input("Enter the message to be encrypted: ")
cipher_text = ""  # stores encrypted texts.
for char in  plain_text:
index = chars.index(char)
cipher_text += keys[index]

print(f"The message to be encrypted is: {plain_text}")
print(f"The encrypted message is: {cipher_text}")