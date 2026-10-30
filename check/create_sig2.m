sig_leng = 16;
Fs = 48000;
%sig_mic = 0.5 * rand(sig_leng,1) - 0.25;
%audiowrite('short_float.wav', single(sig_mic), Fs,'BitsPerSample',32);
[sig_mic1, ~] = audioread('short_float.wav');
