# CryptFile

Original https://github.com/code21acoma/CryptFile<br />
encrypt/decrypt technology (AES)<br />

adjusted for Linux C# / mono / and mono-develop / and testing on Raspberry Pi 3<br />
<hr />

simple <b>compile</b>: <code>mcs crypt.cs</code> > crypt.exe<br />
run / <b>encode</b>: <code>./crypt.exe YOUR_FILE.XYZ</code> > YOUR_FILE.XYZ.aes<br />
run / <b>decode</b>: <code>./crypt.exe YOUR_FILE.XYZ.aes</code><br />
<hr />
<i>
The program is designed for encrypt and decrypt files.<br />
Extension * .aes  is intended only for encrypted files (strictly determined)<br />
-> Software decides - if you want to encrypt or decrypt.<br />
<br />
Notice: Remember your password, otherwise you will really "lose" the original source data!<br />
</i>
<br />
